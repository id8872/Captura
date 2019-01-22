﻿using FFmpeg.AutoGen;

namespace Captura.FFmpeg.Interop
{
    public class FFmpegAudioCodecInfo : FFmpegCodecInfo
    {
        public FFmpegAudioCodecInfo(AVCodecID Id, AVSampleFormat SampleFormat) : base(Id)
        {
            this.SampleFormat = SampleFormat;
        }

        public FFmpegAudioCodecInfo(string Name, AVSampleFormat SampleFormat) : base(Name)
        {
            this.SampleFormat = SampleFormat;
        }

        public AVSampleFormat SampleFormat { get; }
    }
}