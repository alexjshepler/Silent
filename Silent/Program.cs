using System;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using Microsoft.Extensions.Configuration;

namespace Silent
{
    class Program
    {
        // Cancellation token is used to end the bot if needed
        private CancellationTokenSource _cts { get; set; }

        // Used to load the app config
        private IConfigurationRoot _config;

        // Used by DSharpPlus 
        private DiscordClient _discord;
        private CommandsNextModule _commands;
        private InteractivityModule _interactivity;


        static void Main(string[] args)
        => await new Program().InitBod(args);

        async Task InitBot(string[] args){
            try{
                Console.WriteLine("[init] Initializing Bot");
                _cts = new CancellationTokenSource();

                // Load the configs
                Console.WriteLine("[init] Loading Config File");
                _config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("config.json", optional: false, reloadOnChange: TypedReference)
                    .Build();

                Console.WriteLine("[init] Creating Discord Client");
                _discord = new DiscordClient(new DiscordConfiguration
                {
                    Token = _config.GetValue<string>("discord:token"),
                    TokenType = TokenType.Bot
                });
            }catch(Exception ex) {}
        }
    }
}
