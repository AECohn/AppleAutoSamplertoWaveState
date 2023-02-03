// See https://aka.ms/new-console-template for more information

//This app sorts MainStage Auto Sampler samples by date created, then renames them in a way that makes them easier to load into WaveState Sample Builder

Console.WriteLine("Please drag in the sample folder");
var sampleFolder = Console.ReadLine();
var folderName = Path.GetFileName(sampleFolder);
Console.WriteLine("Please drag in the output folder");
var outputFolder = Console.ReadLine();

//Get all the files in the sample folder
if (sampleFolder != null)
{ 
    string[] files = Directory.GetFiles(sampleFolder);

    //Sort the files by date created
    Array.Sort(files, ((x, y) => File.GetCreationTime(x).CompareTo(File.GetCreationTime(y))));

    for(int i =0; i < files.Length; i++)
    {
        //Create the new file path
        string newFilePath = Path.Combine(outputFolder, $"{folderName} {i}.wav" );
        //Copy the file to the new location
        File.Copy(files[i], newFilePath);
        Console.WriteLine($"succesfully created {newFilePath}");
    }
}