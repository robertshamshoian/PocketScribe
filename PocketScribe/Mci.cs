using System;
using System.Runtime.InteropServices;

//...

internal static class Mci
{

  internal const string DllName = "winmm.dll";
  internal const string SoundRecordingDeviceId = "soundRecordingDevice"; // any valid identifier
  internal const string OpenCommandFormat = "open new type waveaudio alias {0}";
  internal const string RecordCommandFormat = "record {0}";
  internal const string PauseCommandFormat = "pause {0}";
  internal const string StopCommandFormat = "stop {0}";
  internal const string CloseCommandFormat = "close {0}";
  internal const string SaveCommandFormatFormat = @"save {0} ""{{0}}""";
  internal static readonly string OpenRecorderCommand = string.Format(OpenCommandFormat, SoundRecordingDeviceId);
  internal static readonly string RecordCommand = string.Format(RecordCommandFormat, SoundRecordingDeviceId);
  internal static readonly string PauseCommand = string.Format(PauseCommandFormat, SoundRecordingDeviceId);
  internal static readonly string StopCommand = string.Format(StopCommandFormat, SoundRecordingDeviceId);
  internal static readonly string CloseRecorderCommand = string.Format(CloseCommandFormat, SoundRecordingDeviceId);
  internal static readonly string SaveCommandFormat = string.Format(SaveCommandFormatFormat, SoundRecordingDeviceId);

  [DllImport(DllName)]
  private static extern long mciSendString(
      string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr oCallback);

  internal static void Open()
  {
    mciSendString(OpenRecorderCommand, null, 0, IntPtr.Zero);
  } //Open

  internal static void Record()
  {
    mciSendString(RecordCommand, null, 0, IntPtr.Zero);
  } //Record

  internal static void Pause()
  {
    mciSendString(PauseCommand, null, 0, IntPtr.Zero);
  } //Pause

  internal static void Stop()
  {
    mciSendString(StopCommand, null, 0, IntPtr.Zero);
  } //Stop

  internal static void Close()
  {
    mciSendString(CloseRecorderCommand, null, 0, IntPtr.Zero);
  } //Close

  internal static void SaveRecording(string fileName)
  {
    mciSendString(string.Format(SaveCommandFormat, fileName), null, 0, IntPtr.Zero);
  } //SaveRecording

}  // classs Mci