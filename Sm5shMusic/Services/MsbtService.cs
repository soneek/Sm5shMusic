﻿using Microsoft.Extensions.Logging;
using MsbtEditor;
using Sm5shMusic.Interfaces;
using Sm5shMusic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sm5shMusic.Services
{
    public class MsbtService : IMsbtService
    {
        private readonly ILogger _logger;

        public MsbtService(ILogger<IMsbtService> logger)
        {
            _logger = logger;
        }

        public bool GenerateNewEntries(List<MsbtNewEntryModel> newMsbtEntries, string inputMsbtFile, string outputMsbtFile)
        {
            try
            {
                File.Copy(inputMsbtFile, outputMsbtFile);
                var msbtFile = new MSBT(outputMsbtFile);
                foreach (var newMsbtEntry in newMsbtEntries)
                {
                    var newEntry = msbtFile.AddLabel(newMsbtEntry.Label);
                    newEntry.Value = Encoding.Unicode.GetBytes(newMsbtEntry.Value + "\0");
                }
                msbtFile.Save();
            }
            catch(Exception e)
            {
                _logger.LogError(e, "MSBT Generation error");
            }

            return true;
        }
    }
}
