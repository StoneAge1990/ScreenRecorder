﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenRecorder.Config;
using ScreenRecorder.Encoder;

namespace ScreenRecorder
{
	public sealed class AppConfig : NotifyPropertyBase, IConfigFile, IDisposable
	{
		private static readonly string AppDataConfigFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ScreenRecorder";

		#region 생성자
		private static volatile AppConfig instance;
		private static object syncRoot = new object();
		public static AppConfig Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
						{
							instance = new AppConfig();
						}
					}
				}

				return instance;
			}
		}

		private readonly string ConfigFilePath = System.IO.Path.Combine(AppConfig.AppDataConfigFolderPath, "config");

		private Object SyncObject = new object();
		private ConfigFileSaveWorker configFileSaveWorker;
		private volatile bool isDisposed = false;

		private AppConfig()
		{
			try
			{
				Load(ConfigFilePath);
			}
			catch { }

			Validation();

			configFileSaveWorker = new ConfigFileSaveWorker(this, ConfigFilePath);
		}
		#endregion

		#region IConfigFile
		public void Save(string filePath)
		{
			lock (this)
			{
				Dictionary<string, string> config = new Dictionary<string, string>();
				config.Add(nameof(ScreenCaptureMonitor), ScreenCaptureMonitor);
				config.Add(nameof(ScreenCaptureCursorVisible), ScreenCaptureCursorVisible.ToString());

				config.Add(nameof(SelectedRecordFormat), SelectedRecordFormat);
				config.Add(nameof(SelectedRecordVideoBitrate), SelectedRecordVideoBitrate.ToString());
				config.Add(nameof(RecordDirectory), RecordDirectory);

				config.Add(nameof(WindowWidth), WindowWidth.ToString());
				config.Add(nameof(WindowHeight), WindowHeight.ToString());
				config.Add(nameof(WindowLeft), WindowLeft.ToString());
				config.Add(nameof(WindowTop), WindowTop.ToString());

				Config.Config.SaveToFile(filePath, config);
			}
		}

		public void Load(string filePath)
		{
			lock (this)
			{
				Dictionary<string, string> config = Config.Config.LoadFromFile(filePath, true);

				if (config != null)
				{
					ScreenCaptureMonitor = Config.Config.GetString(config, nameof(ScreenCaptureMonitor), "");
					ScreenCaptureCursorVisible = Config.Config.GetBool(config, nameof(ScreenCaptureCursorVisible), false);

					SelectedRecordFormat = Config.Config.GetString(config, nameof(SelectedRecordFormat), "mp4");
					SelectedRecordVideoBitrate = Config.Config.GetInt32(config, nameof(SelectedRecordVideoBitrate), 5000000);
					RecordDirectory = Config.Config.GetString(config, nameof(RecordDirectory), Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));

					WindowWidth = Config.Config.GetDouble(config, nameof(WindowWidth), -1.0d);
					WindowHeight = Config.Config.GetDouble(config, nameof(WindowHeight), -1.0d);
					WindowLeft = Config.Config.GetDouble(config, nameof(WindowLeft), -1.0d);
					WindowTop = Config.Config.GetDouble(config, nameof(WindowTop), -1.0d);
				}
				else
				{
					SetDefault();
				}
			}
		}

		public void SetDefault()
		{
			WindowWidth = -1.0d;
			WindowHeight = -1.0d;
			WindowLeft = -1.0d;
			WindowTop = -1.0d;

			ScreenCaptureMonitor = "";
			ScreenCaptureCursorVisible = false;

			SelectedRecordFormat = "mp4";
			SelectedRecordVideoBitrate = 5000000;
			RecordDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
		}
		#endregion

		public void Validation()
		{
			lock (this)
			{
				//if (string.IsNullOrWhiteSpace(SelectedRecordFormat))
				//	SelectedRecordFormat = EncoderFormat.GetRecordFormats()[0].Name;
				//if (SelectedRecordVideoBitrate <= 0)
				//	SelectedRecordVideoBitrate = 5000000;
				//if (SelectedRecordAudioBitrate <= 0)
				//	SelectedRecordAudioBitrate = 160000;
				//if (SelectedRecordVideoSize == null)
				//	SelectedRecordVideoSize = new VideoSize(1920, 1080);
				//if(!string.IsNullOrWhiteSpace(RecordDirectory) && !System.IO.Directory.Exists(RecordDirectory))
				//{
				//	try
				//	{
				//		System.IO.Directory.CreateDirectory(RecordDirectory);
				//	}
				//	catch { }
				//}
			}
		}

		#region Property

		#region Window
		private double windowWidth;
		public double WindowWidth
		{
			get => windowWidth;
            set => SetProperty(ref windowWidth, value);
		}

		private double windowHeight;
		public double WindowHeight
		{
			get => WindowHeight;
            set => SetProperty(ref windowHeight, value);
		}

		private double windowLeft;
		public double WindowLeft
		{
			get => windowLeft;
            set => SetProperty(ref windowLeft, value);
		}

		private double windowTop;
		public double WindowTop
		{
			get => windowTop;
            set => SetProperty(ref windowTop, value);
		}
		#endregion

		#region Record
		private string selectedRecordFormat;
		public string SelectedRecordFormat
		{
			get => selectedRecordFormat;
            set => SetProperty(ref selectedRecordFormat, value);
		}

		private int selectedRecordVideoBitrate;
		public int SelectedRecordVideoBitrate
		{
			get => selectedRecordVideoBitrate;
            set => SetProperty(ref selectedRecordVideoBitrate, value);
		}

		private string recordDirectory;
		public string RecordDirectory
		{
			get => RecordDirectory;
			set => SetProperty(ref recordDirectory, value);
		}
		#endregion

		#region ScreenCapture
		private string screenCaptureMonitor;
		public string ScreenCaptureMonitor
		{
			get => screenCaptureMonitor;
            set => SetProperty(ref screenCaptureMonitor, value);
		}

		private bool screenCaptureCursorVisible;
		public bool ScreenCaptureCursorVisible
		{
			get => screenCaptureCursorVisible;
            set => SetProperty(ref screenCaptureCursorVisible, value);
		}
		#endregion

		#endregion

		public void Dispose()
		{
			try
			{
				lock (SyncObject)
				{
					if (isDisposed)
						return;

					configFileSaveWorker?.Dispose();
					configFileSaveWorker = null;

					isDisposed = true;
				}
			}
			catch { }
		}
	}
}