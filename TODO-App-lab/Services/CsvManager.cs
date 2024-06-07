using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Threading;
using TODO_App_lab.Models;
using TODO_App_lab.Services.Interfaces;

namespace TODO_App_lab.Services
{
    public class CsvManager : ICsvManager
    {
        private readonly ITodoItemRepository _repository;

        public CsvManager(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        public async void ExportToCsv()
        {
            try
            {
                var result = await FolderPicker.Default.PickAsync(new());
                if (result.IsSuccessful)
                {
                    await Toast.Make("Eksportuje plik", ToastDuration.Short, 14d)
                                .Show(new CancellationTokenSource().Token);

                    var records = _repository.GetAll();

                    string filePath = Path.Combine(result.Folder.Path, "exported_data.csv");

                    using var writer = new StreamWriter(filePath);
                    // Nagłówek CSV
                    writer.WriteLine("Id,Title,Description,Status,StartTime,EndTime");

                    // Wiersze CSV
                    foreach (var item in records)
                    {
                        var endtime = (item.EndTime.HasValue) ? item.EndTime.Value.ToString("o", CultureInfo.InvariantCulture) : string.Empty;
                        writer.WriteLine($"{item.Id},{item.Title},{item.Description},{item.Status},{item.StartTime.ToString("o", CultureInfo.InvariantCulture)},{endtime}");
                    }

                    await Toast.Make("Plik został pomyślnie wyeksportowany", ToastDuration.Short, 14d)
                                .Show(new CancellationTokenSource().Token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                await Toast.Make("Nie udało się wyeksportować pliku", ToastDuration.Short, 14d)
                            .Show(new CancellationTokenSource().Token);
            }
        }
    }
}
