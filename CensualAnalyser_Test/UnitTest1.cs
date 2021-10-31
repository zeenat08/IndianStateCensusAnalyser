using IndianStateCensusAnalyser;
using IndianStateCensusAnalyser.DTO;
using NUnit.Framework;
using System.Collections.Generic;
using static IndianStateCensusAnalyser.CensusAnalyser;

namespace CensusAnalyserTest
{
    public class Tests
    {

        //CensusAnalyser.CensusAnalyser censusAnalyser;

        static string indianStateCensusHeaders = "State,Population,AreaInSqKm,DensityPerSqKm";
        static string indianStateCodeHeaders = "SrNo,State Name,TIN,StateCode";

        static string indianStateCensusFilePath = @"C:\Users\Afiyat Khan\source\repos\IndianStateCensusAnalyser\CensualAnalyser_Test\CSV_Files\IndiaStateCensusData.csv";
        static string indianStateCodeFilePath = @"C:\Users\Afiyat Khan\source\repos\IndianStateCensusAnalyser\CensualAnalyser_Test\CSV_Files\IndiaStateCode.csv";

        static string wrongHeaderIndianCensusFilePath = @"C:\Users\Afiyat Khan\source\repos\IndianStateCensusAnalyser\CensualAnalyser_Test\CSV_Files\WrongIndiaStateCensusData.csv";
        static string wrongHeaderStateCodeFilePath = @"C:\Users\Afiyat Khan\source\repos\IndianStateCensusAnalyser\CensualAnalyser_Test\CSV_Files\WrongIndiaStateCode.csv";

        static string wrongIndianStateCensusFilePath = @"C:\Users\Afiyat Khan\source\repos\IndianStateCensusAnalyser\CensualAnalyser_Test\CSV_Files\WrongIndiaStateCensusData1.csv"; //giving wrong input as WrongIndiaStateCensusData1.csv is not present in our file 
        static string wrongIndianStateCodeFilePath = @"C:\Users\Afiyat Khan\source\repos\IndianStateCensusAnalyser\CensualAnalyser_Test\CSV_Files\WrongIndiaStateCode1.csv"; //giving wrong input as WrongIndiaStateCensusData1.csv is not present in our file 

        static string wrongIndianStateCensusFileType = @"C:\Users\Afiyat Khan\source\repos\IndianStateCensusAnalyser\CensualAnalyser_Test\CSV_Files\IndiaStateCensusData.txt"; //require csv file but given txt (file type error)
        static string wrongIndianStateCodeFileType = @"C:\Users\Afiyat Khan\source\repos\IndianStateCensusAnalyser\CensualAnalyser_Test\CSV_Files\IndiaStateCode.txt"; //require csv file but given txt (file type error)

        static string delimiterIndianCensusFilePath = @"C:\Users\Afiyat Khan\source\repos\IndianStateCensusAnalyser\CensualAnalyser_Test\CSV_Files\DelimiterIndiaStateCensusData.csv";
        static string delimiterIndianStateCodeFilePath = @"C:\Users\Afiyat Khan\source\repos\IndianStateCensusAnalyser\CensualAnalyser_Test\CSV_Files\DelimiterIndiaStateCode.csv";
        //US Census FilePath
        //static string usCensusHeaders = "State Id,State,Population,Housing units,Total area,Water area,Land area,Population Density,Housing Density";
        //static string usCensusFilepath = @"C:\Users\Dell\source\repos\CensusAnalyser\CensusAnalyserTest\CsvFiles\USCsvFiles\USCensusData.csv";
        //static string wrongUSCensusFilePath = @"C:\Users\Dell\source\repos\CensusAnalyser\CensusAnalyserTest\CsvFiles\USCsvFiles\USData.csv";
        //static string wrongUSCensusFileType = @"C:\Users\Dell\source\repos\CensusAnalyser\CensusAnalyserTest\CsvFiles\USCsvFiles\USCensusData.txt";
        //static string wrongHeaderUSCensusFilePath = @"C:\Users\Dell\source\repos\CensusAnalyser\CensusAnalyserTest\CsvFiles\USCsvFiles\WrongHeaderUSCensusData.csv";
        //static string delimeterUSCensusFilePath = @"C:\Users\Dell\source\repos\CensusAnalyser\CensusAnalyserTest\CsvFiles\USCsvFiles\DelimiterUSCensusData.csv";

        CensusAnalyser censusAnalyser;
        Dictionary<string, CensusDTO> totalRecord;
        Dictionary<string, CensusDTO> stateRecord;

        [SetUp]
        public void Setup()
        {
            censusAnalyser = new CensusAnalyser();
            totalRecord = new Dictionary<string, CensusDTO>();
            stateRecord = new Dictionary<string, CensusDTO>();
        }

        /// UC1
        /// <summary>
        /// TC 1.1 --> Given the indian census data file when readed should return census data count.
        /// </summary>
        [Test]
        public void GivenIndianCensusDataFile_WhenReaded_ShouldReturnCensusDataCount()
        {
            totalRecord = censusAnalyser.LoadCensusData(indianStateCensusFilePath, Country.INDIA, indianStateCensusHeaders);
            Assert.AreEqual(29, totalRecord.Count);
        }



        /// <summary>
        /// TC 1.2 --> Given the wrong indian census data file when readed should return custom exception.
        /// </summary>
        [Test]
        public void GivenWrongIndianCensusDataFile_WhenReaded_ShouldReturnCustomException()
        {
            var censusException = Assert.Throws<CensusAnalyserCustomException>(() => censusAnalyser.LoadCensusData(wrongIndianStateCensusFilePath, Country.INDIA, indianStateCensusHeaders));
            Assert.AreEqual(CensusAnalyserCustomException.ExceptionType.FILE_NOT_FOUND, censusException.eType);
        }




        ///
        /// <summary>
        /// TC 1.3 --> Given the State Census CSV File when correct but type incorrect Returns a custom Exception (Passing correct File Path but file type should be incorrect)
        /// </summary>
        [Test]
        public void GivenWrongIndianCensusDataFileType_WhenReaded_ShouldReturnCustomException()
        {
            var censusException = Assert.Throws<CensusAnalyserCustomException>(() => censusAnalyser.LoadCensusData(wrongIndianStateCensusFileType, Country.INDIA, indianStateCensusHeaders));
            Assert.AreEqual(CensusAnalyserCustomException.ExceptionType.INVALID_FILE_TYPE, censusException.eType);
        }



        /// <summary>
        /// TC 1.4 --> Given the State Census CSV File when correct but delimiter incorrect Returns a custom Exception
        /// </summary>
        [Test]
        public void GivenIndianCensusDatafile_WhenDelimeterNotProper_ShouldReturnException()
        {
            var censusException = Assert.Throws<CensusAnalyserCustomException>(() => censusAnalyser.LoadCensusData(delimiterIndianCensusFilePath, Country.INDIA, indianStateCensusHeaders));
            Assert.AreEqual(CensusAnalyserCustomException.ExceptionType.INCORRECT_DELIMITER, censusException.eType);
        }



        /// <summary>
        /// TC 1.5 --> Given the State Census CSV File when correct but csv header incorrect Returns a custom Exception
        /// </summary>
        [Test]
        public void GivenIndianCensusDatafile_WhenHeaderNotProper_ShouldReturnException()
        {
            var censusException = Assert.Throws<CensusAnalyserCustomException>(() => censusAnalyser.LoadCensusData(wrongHeaderIndianCensusFilePath, Country.INDIA, indianStateCensusHeaders));
            Assert.AreEqual(CensusAnalyserCustomException.ExceptionType.INCORRECT_HEADER, censusException.eType);
        }


        /// <summary>
        /// TC 2.1 --> Given the indian state code file when readed should match state record count.
        /// </summary>
        [Test]
        public void GivenIndianStateCodeFile_WhenReaded_ShouldReturnStateDataCount()
        {
            stateRecord = censusAnalyser.LoadCensusData(indianStateCodeFilePath, Country.INDIA, indianStateCodeHeaders);
            Assert.AreEqual(37, stateRecord.Count);
        }


        /// <summary>
        /// TC 2.2 --> Given the wrong indian census data file when readed should return custom exception.
        /// </summary>
        [Test]
        public void GivenWrongIndianStateCodeFile_WhenReaded_ShouldReturnCustomException()
        {
            var stateException = Assert.Throws<CensusAnalyserCustomException>(() => censusAnalyser.LoadCensusData(wrongIndianStateCodeFilePath, Country.INDIA, indianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserCustomException.ExceptionType.FILE_NOT_FOUND, stateException.eType);
        }



        /// <summary>
        /// TC 2.3 --> Given the State code CSV File when correct but type incorrect Returns a custom Exception (Passing correct File Path but file type should be incorrect)
        /// </summary>
        [Test]
        public void GivenWrongIndianStateCodeFileType_WhenReaded_ShouldReturnCustomException()
        {
            var stateException = Assert.Throws<CensusAnalyserCustomException>(() => censusAnalyser.LoadCensusData(wrongIndianStateCodeFileType, Country.INDIA, indianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserCustomException.ExceptionType.INVALID_FILE_TYPE, stateException.eType);
        }



        /// <summary>
        /// TC 2.4 --> Given the State code CSV File when correct but delimiter incorrect Returns a custom Exception
        /// </summary>
        [Test]
        public void GivenIndianStateCodefile_WhenDelimeterNotProper_ShouldReturnException()
        {
            var stateException = Assert.Throws<CensusAnalyserCustomException>(() => censusAnalyser.LoadCensusData(delimiterIndianStateCodeFilePath, Country.INDIA, indianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserCustomException.ExceptionType.INCORRECT_DELIMITER, stateException.eType);
        }



        /// <summary>
        /// TC 2.5 --> Given the State code CSV File when correct but csv header incorrect Returns a custom Exception
        /// </summary>
        [Test]
        public void GivenIndianStateCodefile_WhenHeaderNotProper_ShouldReturnException()
        {
            var stateException = Assert.Throws<CensusAnalyserCustomException>(() => censusAnalyser.LoadCensusData(wrongHeaderStateCodeFilePath, Country.INDIA, indianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserCustomException.ExceptionType.INCORRECT_HEADER, stateException.eType);
        }

    }
}