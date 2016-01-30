# Movie Aggregator
Movie Aggregator helps you to store your favourite movie information in to various formats.
You can save information about the movie like Name, Run Time, Language, Lead Actor and Genre and then export these details into one of the 2 formats, as per choice - “plain text” or “PDF”.
On running the application, user will be asked for information about the movie and if needs to export the data into file.

## How to build
Open the project solution in VS 2015 and build.
You can also build using command prompt. Refer https://msdn.microsoft.com/en-us/library/78f4aasd.aspx.

## How to run
After successful build access “MovieAggregator” project and go to Debug folder and run MovieAggregator.exe.
Feed in the required movie information and then choose export method, PDF or TXT.
On successful export the file will be available under the MovieAggregator/bin/debug folder.
PDF Samples can be seen there.
NOTE: if changes are made to the existing providers the DLLs needs to be replaced into  appropriate folders.

## How to add your own provider
More output formats can be added to the application by writing your own provider using IOutputProvider and adding OutputProviderInfo attribute to your provider class.
Build and place your provider dependencies in application root and provider (.dll) under “Providers” folder, application will automatically pick up your provider next time you run the application.

## How dynamic provider loading works
The application loads up all the DLLs in the Providers folder and check for valid providers with OutputProviderInfo attribute and IOutputProvider interface implemented. 
Once loaded the user is presented with all available options to export the Output method of the provider is called with data in byte[], and the output is created.

For more queries and explanation drop me a mail (sachinnair1990@yahoo.com) or discuss it out on the comment section, I’ll be happy to help.

