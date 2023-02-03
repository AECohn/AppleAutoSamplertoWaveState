// See https://aka.ms/new-console-template for more information

//This app sorts MainStage Auto Sampler samples by date created, then renames them in a way that makes them easier to load into WaveState Sample Builder


using NAudio.Wave;

Console.WriteLine("Please drag in the sample folder");
var sampleFolder = Console.ReadLine()?.Replace(@"\", @"").Trim();
Console.WriteLine(sampleFolder);
var folderName = Path.GetFileName(sampleFolder);
Console.WriteLine("Please drag in the output folder");
var outputFolder = (Console.ReadLine()?.Replace(@"\", @"").Trim());
Console.WriteLine(outputFolder);

//from official documentation, some changes made - https://github.com/naudio/NAudio/blob/master/README.md - https://markheath.net/post/how-to-convert-aiff-files-to-wav-using 
static void ConvertAiffToWav(string aiffFile, string? wavFile)
{
    using (AiffFileReader reader = new AiffFileReader(aiffFile))
    {
        using (WaveFileWriter writer = new WaveFileWriter(wavFile, reader.WaveFormat))
        {
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            do
            {
                bytesRead = reader.Read(buffer, 0, buffer.Length - (buffer.Length % writer.WaveFormat.BlockAlign));
                writer.Write(buffer, 0, bytesRead);
            } while (bytesRead > 0);
        }
    }
}


//Get all the files in the sample folder
if (sampleFolder != null)
{
    string? newFilePath = null;
    string[] files = Directory.GetFiles(sampleFolder);

    //Sort the files by date created
    Array.Sort(files, ((x, y) => File.GetCreationTime(x).CompareTo(File.GetCreationTime(y))));

    for (int i = 0; i < files.Length; i++)
    {
        //Create the new file path
        if (outputFolder != null)
        {
            newFilePath = Path.Combine(outputFolder, $"{folderName} {i}.wav");
        }

        //converts file to wav
        ConvertAiffToWav(files[i], newFilePath);
        Console.WriteLine($"succesfully created {newFilePath}");
    }
}
