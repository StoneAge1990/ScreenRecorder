🌏 [ENGLISH](./README.md)

<img src="./ScreenRecorder/icon.ico" width="100" height="100">

# Screen Recorder

<p>
  <img src="https://hits.seeyoufarm.com/api/count/incr/badge.svg?url=https%3A%2F%2Fgithub.com%2Fakon47%2FScreenRecorder&count_bg=%2379C83D&title_bg=%23555555&icon=&icon_color=%23E7E7E7&title=hits&edge_flat=false" />
  <img alt="GitHub starts" src="https://img.shields.io/github/stars/akon47/ScreenRecorder">
</p>

컴퓨터 화면과 기본 오디오 재생 장치의 오디오를 녹화하는 기능을 가진 녹화 프로그램 입니다.   

### 기본 설정
![screenshot1_ko](https://user-images.githubusercontent.com/49547202/131764241-29e60789-027e-4bd6-b14b-84ef597804d9.png)

### 고급 설정
![screenshot2_ko](https://user-images.githubusercontent.com/49547202/131763723-a045d042-6c39-48b8-beae-483429b18d22.png)

## 📃 사용법
- 동그란 버튼을 누르면 녹화가 시작되고 사각형 버튼을 누르면 정지됩니다.
- 우측 하단의 톱니바퀴 버튼을 누르면 녹화 설정에 대한 팝업메뉴가 표시됩니다.

## 🎨 특징
- 기본적으로 비디오 코덱은 H.264 를 사용합니다.
  - 사용자의 컴퓨터에서 NVENC나 QuickSync 의 하드웨어 코덱을 지원한다면 해당 코덱을 우선적으로 사용합니다.
  - 필요한 경우 고급 설정 메뉴에서 H.265 코덱도 선택이 가능합니다. (하드웨어 인코딩이 지원 안 되는 경우 매우 높은 CPU 로드가 발생할 수 있습니다)
- 기본적으로 오디오 코덱은 AAC 를 사용합니다.
  - 필요한 경우 고급 설정 메뉴에서 MP3 코덱도 선택이 가능합니다.
- 프로그램 자기 자신은 녹화 화면에 포함되지 않고 녹화됩니다.
- 커서 캡쳐 설정을 이용하여 마우스 커서의 캡쳐 여부를 설정할 수 있습니다.

## 📚 레퍼런스
- [v1.0.4 버전에 대한 소프트피디아 에디터의 리뷰](https://www.softpedia.com/get/Multimedia/Video/Video-Recording/ScreenRecorder-K.shtml)

## 👨‍💻 빌드

- Visual Studio 2019 or newer
- Windows 10 or newer
- Microsoft .Net Framework 4.7.2

MediaEncoder 프로젝트에서는 [BtbN](https://github.com/BtbN/FFmpeg-Builds) 에서 빌드하고 배포하는 shared ffmpeg 라이브러리를 사용합니다.   

1. 프로젝트 폴더 안에 **ffmpeg_shared_lib** 폴더를 생성합니다.
2. **fmpeg_shared_lib** 폴더 안에 shared ffmpeg 라이브러리의 "**bin, include, lib**" 폴더를 붙여 넣고 빌드 하시면 됩니다.

## 📦 서드 파티 라이브러리
- SharpDX: http://sharpdx.org/
- NAudio: https://github.com/naudio/NAudio

## 🐞 버그 리포트
만약 버그를 발견하신다면 [issues](https://github.com/akon47/ScreenRecorder/issues) 로 보고해 주세요.