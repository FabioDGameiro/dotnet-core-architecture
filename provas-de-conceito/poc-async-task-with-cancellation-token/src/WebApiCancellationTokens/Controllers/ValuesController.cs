using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApiCancellationTokens.Controllers
{
    [Route("api/values")]
    public class ValuesController : Controller
    {
        private readonly ILogger _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }


        [HttpGet("normal")]
        public async Task<string> GetNormal()
        {
            var result = "Task Completed";
            await Task.Delay(10000);

            _logger.LogWarning(result);

            return result;
        }

        [HttpGet("cancelable")]
        public async Task<string> GetCancelable(CancellationToken cancellationToken)
        {
            var result = "Task Completed";
            await Task.Delay(10000, cancellationToken);

            _logger.LogWarning(result);

            return result;
        }


        [HttpGet("normal-loop")]
        public async Task<string> GetCancelableLoop()
        {
            _logger.LogWarning("Starting to do slow work");

            for(var i=0; i<10; i++)
            {
                await Task.Delay(1000);
                _logger.LogWarning($"Looping {i}");
            }

            _logger.LogWarning("Finished task");
            return "Finished task";
        }

        [HttpGet("cancelable-loop")]
        public string GetCancelableLoop(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogWarning("Starting to do slow work");

                for(var i=0; i<10; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    
                    Thread.Sleep(1000);
                    _logger.LogWarning($"Looping {i}");
                }

                _logger.LogWarning("Finished task");
                return "Finished task";
            }
            catch(OperationCanceledException)
            {
                _logger.LogWarning("Operação cancelada");
                return "Operação cancelada";
            }

        }

        [HttpGet("cancelable-loop-async")]
        public async Task<string> GetCancelableLoopAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogWarning("Starting to do slow work");

                for(var i=0; i<10; i++)
                {
                    await Task.Delay(1000,cancellationToken);
                    _logger.LogWarning($"Looping {i}");
                }

                _logger.LogWarning("Finished async task");
                return "Finished async task";
            }
            catch(OperationCanceledException)
            {
                _logger.LogWarning("Operação cancelada");
                return "Operação cancelada";
            }

        }
    }
}
