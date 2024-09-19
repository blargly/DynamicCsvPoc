using CsvHelper;
using CSVUploader.Models;
using System.Globalization;

namespace CSVUploader.Services
{
    public class CsvService
    {
        private const string IdField = "Barcode ID";
        private const string GroupField = "Group / class";

        public List<SurveyInputModel> Convert(IFormFile file)
        {
            var surveyInputModels = new List<SurveyInputModel>();

            using (var stream = file.OpenReadStream())
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.Read();
                csv.ReadHeader();
                var headers = csv.HeaderRecord;

                while (csv.Read())
                {
                    var surveyInput = new SurveyInputModel
                    {
                        Id = csv.GetField<int>(IdField),
                        Group = csv.GetField<string>(GroupField)
                    };

                    //build the questons dictionary
                    for (int i = 0; i < headers.Length; i++)
                    {
                        var columnName = headers[i];
                        if (columnName != IdField && columnName != GroupField)
                        {
                            var columnValue = csv.GetField<string>(columnName) ?? string.Empty;
                            surveyInput.Questions.Add(columnName, columnValue);
                        }
                    }

                    surveyInputModels.Add(surveyInput);

                }
            }
            return surveyInputModels;
        }
    }
}
