using System;
using System.ClientModel;
//using System.ClientModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenAI;
using OpenAI.Audio;
using OpenAI.Chat;
using NAudio.Wave;
//using OpenAI;
//using OpenAI.Audio;
//using OpenAI.Chat;


namespace HtmlParser.Language.Extensions
{
    public class GPT
    {
        //private const string modelName = "gpt-4o-mini";
        //private const string modelName = "gpt-5-mini";
        
        private const string modelName = "gpt-4.1-mini";
        private const string audioModelName = "gpt-4o-mini-tts";

        public void GenerateSound(string word, IList<DeRuItem> words, string outputFile = null,
            int pauseMilliseconds = 2000, GeneratedSpeechVoice? voice = null)
        {

            if (string.IsNullOrEmpty(outputFile))
            {
                outputFile = $"D:\\Speech to Text\\{word}.mp3";
            }

            if (!voice.HasValue)
            {
                voice = GeneratedSpeechVoice.Alloy;
            }


            WaveFormat format = new WaveFormat(24000, 16, 1);

            using (WaveFileWriter writer = new WaveFileWriter("temp.wav", format))
            {
                int i;

                for (i = 0; i < words.Count; i++)
                {
                    byte[] german = GenerateSpeechBytes(
                        words[i].De,
                        "Произноси как носитель немецкого языка.",
                        voice.Value);

                    AppendMp3Bytes(writer, german);

                    AppendSilence(writer, pauseMilliseconds);

                    byte[] russian = GenerateSpeechBytes(
                        words[i].Ru,
                        "Произноси как носитель русского языка.",
                        voice.Value);

                    AppendMp3Bytes(writer, russian);

                    if (i != words.Count - 1)
                        AppendSilence(writer, pauseMilliseconds);
                }
            }

            using (WaveFileReader reader = new WaveFileReader("temp.wav"))
            {
                MediaFoundationEncoder.EncodeToMp3(reader, outputFile);
            }
        }


        private byte[] GenerateSpeechBytes(string text, string instructions, GeneratedSpeechVoice voice)
        {
            SpeechGenerationOptions options = new SpeechGenerationOptions();
            options.ResponseFormat = GeneratedSpeechFormat.Mp3;
            //options.Instructions = instructions;

            var client = new OpenAIClient(key);

            var speech = client.GetAudioClient(audioModelName);

            ClientResult<BinaryData> result = speech.GenerateSpeech(
                text,
                voice,
                options);

            return result.Value.ToArray();
        }

        private void AppendMp3Bytes(WaveFileWriter writer, byte[] mp3Bytes)
        {
            using (MemoryStream mp3Stream = new MemoryStream(mp3Bytes))
            using (Mp3FileReader reader = new Mp3FileReader(mp3Stream))
            using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(reader))
            using (var resampled = new MediaFoundationResampler(pcm, writer.WaveFormat))
            {
                byte[] buffer = new byte[4096];
                int bytesRead;

                while ((bytesRead = resampled.Read(buffer, 0, buffer.Length)) > 0)
                {
                    writer.Write(buffer, 0, bytesRead);
                }
            }
        }

        private void AppendSilence(WaveFileWriter writer, int milliseconds)
        {
            int bytesPerMillisecond = writer.WaveFormat.AverageBytesPerSecond / 1000;
            int silenceBytesCount = bytesPerMillisecond * milliseconds;

            byte[] silence = new byte[silenceBytesCount];
            writer.Write(silence, 0, silence.Length);
        }



        public string GetResponse(string request)
        {
            try
            {
                var client = new ChatClient(modelName, key);

                var response = client.CompleteChat(request);


                var text = response.Value.Content.FirstOrDefault()?.Text
                    .Replace("Пример:", "")
                    .Replace("Перевод:", "")
                    .Replace("**Русский:**", "")
                    .Replace("**Deutsch:**", "")
                    .Replace("\"", "")
                    .Replace("\r\n", " - ")
                    .Replace("\r", " - ")
                    .Replace("\n", " - ")
                    .Replace("  ", " ")
                    .Replace("  ", " ")
                    .Trim();

                return text;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }

            return "";

        }


    }
}
