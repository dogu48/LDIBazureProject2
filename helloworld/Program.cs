
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

namespace helloworld
{
    class Program
    {
        static string speechKey = Environment.GetEnvironmentVariable("SPEECH_KEY");
        static string speechRegion = Environment.GetEnvironmentVariable("SPEECH_REGION");
        public static async Task SynthesisToSpeakerAsync()
        {
          
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                Console.InputEncoding = System.Text.Encoding.Unicode;
                Console.OutputEncoding = System.Text.Encoding.Unicode;
            }

            var config = SpeechConfig.FromSubscription(speechKey, speechRegion);

           
            config.SpeechSynthesisVoiceName = "en-US-AriaNeural";

            
            using (var synthesizer = new SpeechSynthesizer(config))
            {
               
                Console.WriteLine(" Hello Welcome I will read what you give me ");
                Console.Write("> ");
                string text = System.IO.File.ReadAllText(@"C:\Users\Dogukan\Desktop\data-all.txt");

                while (true)
                {
                    using (var result = await synthesizer.SpeakTextAsync(text))
                    {
                        if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                        {
                            Console.WriteLine($"Speech synthesized to speaker for text [{text}]");
                        }
                        else if (result.Reason == ResultReason.Canceled)
                        {
                            var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                            Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                            if (cancellation.Reason == CancellationReason.Error)
                            {
                                Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                                Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                                Console.WriteLine($"CANCELED: Did you update the subscription info?");
                            }
                        }
                    }
                    Console.WriteLine("Press 1 to continue ,any key different from 1 to exit");
                    string x = Console.ReadLine();
                    if (x == "1")
                    {
                        Console.WriteLine("Write another sentence for me to speak");
                        text = Console.ReadLine();
                        continue;
                        
                    }
                    else
                    {
                        break;
                    }

                }
                
                Console.ReadKey();
            }
        }

        static async Task Main()
        {
            await SynthesisToSpeakerAsync();
        }
    }
}

