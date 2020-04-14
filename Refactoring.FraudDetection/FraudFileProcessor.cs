using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using TinyCsvParser;

namespace Refactoring.FraudDetection
{
    //Testeable with MockFileSystem();
    //https://dontcodetired.com/blog/post/Unit-Testing-C-File-Access-Code-with-SystemIOAbstractions
    public class FraudFileProcessor
    {

        private readonly IFileSystem _fileSystem;

        private HashSet<Order> _fraudLines { get; set; }
        public HashSet<Order> FraudLines
        {
            get
            {
                return _fraudLines;
            }
        }

        public FraudFileProcessor() : this(new FileSystem())
        {
            _fraudLines = new HashSet<Order>();
        }

        public FraudFileProcessor(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            _fraudLines = new HashSet<Order>();
        }

        public void ReadFraudFile(string inputFilePath)
        {
            try
            {
                using (Stream stream = _fileSystem.File.OpenRead(inputFilePath))
                {
                    CsvParserOptions options = new CsvParserOptions(false, ',');
                    var csvParser = new CsvParser<Order>(options, new OrderMapping());
                    var records = csvParser.ReadFromStream(stream, Encoding.UTF8);
                    _fraudLines = records.Select(x => x.Result).ToHashSet();
                }
            }
            catch (IOException)
            {
                //TODO Log
            }
            catch (ArgumentNullException)
            {
                //TODO Log
            }
            catch (OperationCanceledException)
            {
                //TODO Log
            }
            catch (AggregateException)
            {
                //TODO Log
            }
            catch (Exception)
            {
                //TODO Log
            }
        }

    }
}
