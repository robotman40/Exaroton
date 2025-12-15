using System.Text.Json;


/// <summary>
///  The Exaroton class provides methods to interact with the Exaroton API.
/// </summary>
public class Exaroton
{
    private HttpClient sharedClient; // Shared HttpClient instance for making API requests
    private const string baseUri = "https://api.exaroton.com/v1"; // Base URI for the Exaroton API

    /// <summary>
    /// JSON serializer options to handle camelCase property naming.
    /// </summary>
    private readonly JsonSerializerOptions jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
    
    /// <summary>
    /// Constructor for the Exaroton class.
    /// Initializes the HttpClient with the provided API key.
    /// </summary>
    /// <param name="apiKey"></param>
    public Exaroton(string apiKey)
    {
        sharedClient = new HttpClient(); // Initialize the shared HttpClient
        sharedClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}"); // Add the API key to the request headers
    }

    /// <summary>
    /// Helper function that makes a GET request to the specified API path.
    /// </summary>
    /// <param name="path"></param>
    private async Task<string> GetRequest(string path)
    {
        string responseString;
        try
        {
            var response = await sharedClient.GetAsync($"{baseUri}/{path}"); // Make the GET request
            responseString = await response.Content.ReadAsStringAsync(); // Read the response content as a string
        }
        catch (HttpRequestException e)
        {
            throw new ExarotonException($"An error occurred making a GET request: {e.Message}");
        }

        return responseString;
    }
    
    /// <summary>
    /// Helper function that makes a POST request to the specified API path.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="content"></param>
    private async Task<string> PostRequest(string path, HttpContent content)
    {
        string responseString;
        try
        {
            var response = await sharedClient.PostAsync($"{baseUri}/{path}", content); // Make the POST request
            responseString = await response.Content.ReadAsStringAsync(); // Read the response content as a string
        }
        catch (Exception e)
        {
            throw new ExarotonException($"An error occurred making a POST request: {e.Message}");
        }

        return responseString;
    }

    /// <summary>
    /// Helper function that makes a PUT request to the specified API path.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="content"></param>
    private async Task<string> PutRequest(string path, HttpContent content)
    {
        string responseString;
        try
        {
            var response = await sharedClient.PutAsync($"{baseUri}/{path}", content); // Make the PUT request
            responseString = await response.Content.ReadAsStringAsync(); // Read the response content as a string
        }
        catch (Exception e)
        {
            throw new ExarotonException($"An error occurred making a PUT request: {e.Message}");
        }

        return responseString;
    }

    /// <summary>
    /// Helper function that makes a DELETE request to the specified API path.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="content"></param>
    private async Task<string> DeleteRequest(string path, HttpContent content)
    {
        string responseString;
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Delete, $"{baseUri}/{path}")
                { Content = content }; // Create the DELETE request along with content
            var response = await sharedClient.SendAsync(request); // Send the DELETE request
            responseString = await response.Content.ReadAsStringAsync(); // Read the response content as a string
        }
        catch (Exception e)
        {
            throw new ExarotonException($"An error occurred making a DELETE request: {e.Message}");
        }
        
        return responseString;
    }

    /// <summary>
    /// Get account information.
    /// </summary>
    /// <returns>AccountResponse</returns>
    public AccountResponse GetAccount()
    {
        string responseString = GetRequest("account/").Result; // Make the GET request to the account endpoint
        var account = JsonSerializer.Deserialize<AccountResponse>(responseString, jsonOptions); // Deserialize the response into an AccountResponse object
        return account!;
    }

    /// <summary>
    /// Get a list of servers.
    /// </summary>
    /// <returns>ServersResponse</returns>
    public ServersResponse GetServers()
    {
        string responseString = GetRequest("servers/").Result; // Make the GET request to the servers endpoint
        var servers = JsonSerializer.Deserialize<ServersResponse>(responseString, jsonOptions); // Deserialize the response into a ServersResponse object
        return servers!;
    }

    /// <summary>
    /// Get information about a specific server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>ServerResponse</returns>
    public ServerResponse GetServer(string serverId)
    {
        string responseString = GetRequest($"servers/{serverId}/").Result; // Make the GET request to the specific server endpoint
        var serverData = JsonSerializer.Deserialize<ServerResponse>(responseString, jsonOptions); // Deserialize the response into a ServerResponse object
        return serverData!;
    }

    /// <summary>
    /// Get information about a specific server.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>ServerResponse</returns>
    public ServerResponse GetServer(Server server)
    {
        string responseString = GetRequest($"servers/{server.Id}/").Result; // Make the GET request to the specific server endpoint
        var serverData = JsonSerializer.Deserialize<ServerResponse>(responseString, jsonOptions); // Deserialize the response into a ServerResponse object
        return serverData!;
    }

    /// <summary>
    /// Get the server log.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>LogResponse</returns>
    public LogResponse GetServerLog(string serverId)
    {
        string responseString = GetRequest($"servers/{serverId}/logs/").Result; // Make the GET request to fetch the server log
        var log = JsonSerializer.Deserialize<LogResponse>(responseString, jsonOptions); // Deserialize the response into a LogResponse object
        return log!;
    }

    /// <summary>
    /// Get the server log.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>LogResponse</returns>
    public LogResponse GetServerLog(Server server)
    {
        string responseString = GetRequest($"servers/{server.Id}/logs/").Result; // Make the GET request to fetch the server log
        var log = JsonSerializer.Deserialize<LogResponse>(responseString, jsonOptions); // Deserialize the response into a LogResponse object
        return log!;
    }

    /// <summary>
    /// Upload the server log.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>UploadedLogResponse</returns>
    public UploadedLogResponse UploadServerLog(string serverId)
    {
        string responseString = GetRequest($"servers/{serverId}/logs/share/").Result; // Make the GET request to upload the server log
        var log = JsonSerializer.Deserialize<UploadedLogResponse>(responseString, jsonOptions); // Deserialize the response into an UploadedLogResponse object
        return log!;
    }
 
    /// <summary>
    /// Upload the server log.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>UploadedLogResponse</returns>
    public UploadedLogResponse UploadServerLog(Server server)
    {
        string responseString = GetRequest($"servers/{server.Id}/logs/share/").Result; // Make the GET request to upload the server log
        var log = JsonSerializer.Deserialize<UploadedLogResponse>(responseString, jsonOptions); // Deserialize the response into an UploadedLogResponse object
        return log!;
    }

   /// <summary>
   /// Get the RAM of the specified server.
   /// </summary>
   /// <param name="serverId"></param>
   /// <returns>RamResponse</returns>
    public RamResponse GetServerRam(string serverId)
    {
        string responseString = GetRequest($"servers/{serverId}/options/ram/").Result; // Make the GET request to fetch the server RAM option
        var ram = JsonSerializer.Deserialize<RamResponse>(responseString, jsonOptions); // Deserialize the response into a RamResponse object
        return ram!;
    }

    /// <summary>
    /// Get the RAM of the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>RamResponse</returns>
    public RamResponse GetServerRam(Server server)
    {
        string responseString = GetRequest($"servers/{server.Id}/options/ram/").Result; // Make the GET request to fetch the server RAM option
        var ram = JsonSerializer.Deserialize<RamResponse>(responseString, jsonOptions); // Deserialize the response into a RamResponse object
        return ram!;
    }
  
    /// <summary>
    /// Change the RAM of the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="serverRam"></param>
    /// <returns>ChangeRamResponse</returns>
    public ChangeRamResponse ChangeServerRam(string serverId, int serverRam)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { ram = serverRam }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = PostRequest($"servers/{serverId}/options/ram/", content).Result; // Make the POST request to change the server RAM option
        var changeRam = JsonSerializer.Deserialize<ChangeRamResponse>(responseString, jsonOptions); // Deserialize the response into a ChangeRamResponse object
        return changeRam!;
    }

    /// <summary>
    /// Change the RAM of the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="ram"></param>
    /// <returns>ChangeRamResponse</returns>
    public ChangeRamResponse ChangeServerRam(Server server, int ram)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { ram = ram }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = PostRequest($"servers/{server.Id}/options/ram/", content).Result; // Make the POST request to change the server RAM option
        var changeRam = JsonSerializer.Deserialize<ChangeRamResponse>(responseString, jsonOptions); // Deserialize the response into a ChangeRamResponse object
        return changeRam!;
    }

    /// <summary>
    /// Get the MOTD of the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>MotdResponse</returns>
    public MotdResponse GetServerMotd(string serverId)
    {
        string responseString = GetRequest($"servers/{serverId}/options/motd/").Result; // Make the GET request to fetch the server MOTD option
        var motd = JsonSerializer.Deserialize<MotdResponse>(responseString, jsonOptions); // Deserialize the response into a MotdResponse object
        return motd!;
    }

    /// <summary>
    /// Get the MOTD of the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>MotdResponse</returns>
    public MotdResponse GetServerMotd(Server server)
    {
        string responseString = GetRequest($"servers/{server.Id}/options/motd/").Result; // Make the GET request to fetch the server MOTD option
        var motd = JsonSerializer.Deserialize<MotdResponse>(responseString, jsonOptions); // Deserialize the response into a MotdResponse object
        return motd!;
    }
   
    /// <summary>
    /// Change the MOTD of the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="serverMotd"></param>
    /// <returns>ChangeMotdResponse</returns>
    public ChangeMotdResponse ChangeServerMotd(string serverId, string serverMotd)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { motd = serverMotd }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = PostRequest($"servers/{serverId}/options/motd/", content).Result; // Make the POST request to change the server MOTD option
        var changeMotd = JsonSerializer.Deserialize<ChangeMotdResponse>(responseString, jsonOptions); // Deserialize the response into a ChangeMotdResponse object
        return changeMotd!;
    }
  
    /// <summary>
    /// Change the MOTD of the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="motd"></param>
    /// <returns>ChangeMotdResponse</returns>
    public ChangeMotdResponse ChangeServerMotd(Server server, string motd)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { motd = motd }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = PostRequest($"servers/{server.Id}/options/motd/", content).Result; // Make the POST request to change the server MOTD option
        var changeMotd = JsonSerializer.Deserialize<ChangeMotdResponse>(responseString, jsonOptions); // Deserialize the response into a ChangeMotdResponse object
        return changeMotd!;
    }

    /// <summary>
    /// Start the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse StartServer(string serverId)
    {
        string responseString = GetRequest($"servers/{serverId}/start/").Result; // Make the GET request to start the server
        var startServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return startServer!;
    }

    /// <summary>
    /// Start the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse StartServer(Server server)
    {
        string responseString = GetRequest($"servers/{server.Id}/start/").Result; // Make the GET request to start the server
        var startServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return startServer!;
    }

    /// <summary>
    /// Start the specified server using own credits.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse StartServerWithOwnCredits(string serverId)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { useOwnCredits = true }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = PostRequest($"servers/{serverId}/start/", content).Result; // Make the POST request to start the server using own credits
        var startServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return startServer!;
    }
 
    /// <summary>
    /// Start the specified server using own credits.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse StartServerWithOwnCredits(Server server)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { useOwnCredits = true }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = PostRequest($"servers/{server.Id}/start/", content).Result; // Make the POST request to start the server using own credits
        var startServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return startServer!;
    }

    /// <summary>
    /// Stop the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse StopServer(string serverId)
    {
        string responseString = GetRequest($"servers/{serverId}/stop/").Result;
        var stopServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions);
        return stopServer!;
    }

    /// <summary>
    /// Stop the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse StopServer(Server server)
    {
        string responseString = GetRequest($"servers/{server.Id}/stop/").Result; // Make the GET request to stop the server
        var stopServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return stopServer!;
    }
    
    /// <summary>
    /// Restart the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse RestartServer(string serverId)
    {
        string responseString = GetRequest($"servers/{serverId}/restart/").Result; // Make the GET request to restart the server
        var restartServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return restartServer!;
    }

    /// <summary>
    /// Restart the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse RestartServer(Server server)
    {
        string responseString = GetRequest($"servers/{server.Id}/restart/").Result; // Make the GET request to restart the server
        var restartServer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return restartServer!;
    }

    /// <summary>
    /// Execute a server command in the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="serverCommand"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse ExecuteServerCommand(string serverId, string serverCommand)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { command = serverCommand }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = PostRequest($"servers/{serverId}/command/", content).Result; // Make the POST request to execute the server command
        var executeCommand = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return executeCommand!;
    }

    /// <summary>
    /// Execute a server command in the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="serverCommand"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse ExecuteServerCommand(Server server, string serverCommand)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { command = serverCommand }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = PostRequest($"servers/{server.Id}/command/", content).Result; // Make the POST request to execute the server command
        var executeCommand = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return executeCommand!;
    }

    /// <summary>
    /// Extend the timer for the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="serverSeconds"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse ExtendServerTimer(string serverId, int serverSeconds)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { time = serverSeconds }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = PostRequest($"servers/{serverId}/extend-time/", content).Result; // Make the POST request to extend the server timer
        var extendTimer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return extendTimer!;
    }

    /// <summary>
    /// Extend the timer for the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="serverSeconds"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse ExtendServerTimer(Server server, int serverSeconds)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { time = serverSeconds }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = PostRequest($"servers/{server.Id}/extend-time/", content).Result; // Make the POST request to extend the server timer
        var extendTimer = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return extendTimer!;
    }
    
    // Player list methods
    
    /// <summary>
    /// Get player lists in the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <returns>ListResponse</returns>
    public ListResponse GetPlayerLists(string serverId)
    {
        string responseString = GetRequest($"servers/{serverId}/playerlists/").Result; // Make the GET request to fetch player lists
        var playerLists = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object
        return playerLists!;
    }
    
    /// <summary>
    /// Get player lists in the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <returns>ListResponse</returns>
    public ListResponse GetPlayerLists(Server server)
    {
        string responseString = GetRequest($"servers/{server.Id}/playerlists/").Result; // Make the GET request to fetch player lists
        var playerLists = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object
        return playerLists!;
    }

    /// <summary>
    /// Get player list contents in the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="list"></param>
    /// <returns>ListResponse</returns>
    public ListResponse GetPlayerListContents(string serverId, string list)
    {
        string responseString = GetRequest($"servers/{serverId}/playerlists/{list}/").Result; // Make the GET request to fetch player list contents
        var playerList = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object
        return playerList!;
    }
    
    /// <summary>
    /// Get player list contents in the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="list"></param>
    /// <returns>ListResponse</returns>
    public ListResponse GetPlayerListContents(Server server, string list)
    {
        string responseString = GetRequest($"servers/{server.Id}/playerlists/{list}/").Result; // Make the GET request to fetch player list contents
        var playerList = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object
        return playerList!;
    }

    /// <summary>
    /// Add entries to a player list in the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="list"></param>
    /// <param name="serverEntries"></param>
    /// <returns>ListResponse</returns>
    public ListResponse AddEntriesToPlayerList(string serverId, string list, List<string> serverEntries)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { entries = serverEntries }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = PutRequest($"servers/{serverId}/playerlists/{list}/", content).Result; // Make the PUT request to add entries to the player list
        var playerEntries = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object

        return playerEntries!;
    }

    /// <summary>
    /// Add entries to a player list in the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="list"></param>
    /// <param name="serverEntries"></param>
    /// <returns>ListResponse</returns>
    public ListResponse AddEntriesToPlayerList(Server server, string list, List<string> serverEntries)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { entries = serverEntries }),
            System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = PutRequest($"servers/{server.Id}/playerlists/{list}/", content).Result; // Make the PUT request to add entries to the player list
        var playerEntries = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object

        return playerEntries!;
    }

    /// <summary>
    /// Remove entries from a player list in the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="list"></param>
    /// <param name="serverEntries"></param>
    /// <returns>ListResponse</returns>
    public ListResponse RemoveEntriesFromPlayerList(string serverId, string list, List<string> serverEntries)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { entries = serverEntries }), System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = DeleteRequest($"servers/{serverId}/playerlists/{list}/", content).Result; // Make the DELETE request to remove entries from the player list
        var playerEntries = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object

        return playerEntries!;
    }

    /// <summary>
    /// Remove entries from a player list in the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="list"></param>
    /// <param name="serverEntries"></param>
    /// <returns>ListResponse</returns>
    public ListResponse RemoveEntriesFromPlayerList(Server server, string list,
        List<string> serverEntries)
    {
        var content = new StringContent(JsonSerializer.Serialize(new { entries = serverEntries }),
            System.Text.Encoding.UTF8, "application/json"); // Create the request content
        string responseString = DeleteRequest($"servers/{server.Id}/playerlists/{list}/", content).Result; // Make the DELETE request to remove entries from the player list
        var playerEntries = JsonSerializer.Deserialize<ListResponse>(responseString, jsonOptions); // Deserialize the response into a ListResponse object

        return playerEntries!;
    }

    // Get file information methods
    
    /// <summary>
    /// Get file information from the specified server and file path.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="filePath"></param>
    /// <returns>FileInformationResponse</returns>
    public FileInformationResponse GetFileInformation(string serverId, string filePath)
    {
        string responseString = GetRequest($"servers/{serverId}/files/info/{filePath}/").Result; // Make the GET request to fetch file information
        var fileInfo = JsonSerializer.Deserialize<FileInformationResponse>(responseString, jsonOptions); // Deserialize the response into a FileInformationResponse object
        return fileInfo!;
    }

    /// <summary>
    /// Get file information from the specified server and file path.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="filePath"></param>
    /// <returns>FileInformationResponse</returns>
    public FileInformationResponse GetFileInformation(Server server, string filePath)
    {
        string responseString = GetRequest($"servers/{server.Id}/files/info/{filePath}/").Result; // Make the GET request to fetch file information
        var fileInfo = JsonSerializer.Deserialize<FileInformationResponse>(responseString, jsonOptions); // Deserialize the response into a FileInformationResponse object
        return fileInfo!;
    }
    
    /// <summary>
    /// Get file information and save it to the specified path.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="filePath"></param>
    /// <param name="savePath"></param>
    public void GetFileData(string serverId, string filePath, string savePath)
    {
        var fileBytes = sharedClient.GetByteArrayAsync($"{baseUri}/servers/{serverId}/files/data/{filePath}/").Result; // Make the GET request to download the file
        File.WriteAllBytes(savePath, fileBytes); // Save the downloaded file to the specified path
    }
    
    /// <summary>
    /// Get file information and save it to the specified path.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="filePath"></param>
    /// <param name="savePath"></param>
    public void GetFileData(Server server, string filePath, string savePath)
    {
        try
        {
            var fileBytes = sharedClient.GetByteArrayAsync($"{baseUri}/servers/{server.Id}/files/data/{filePath}/")
                .Result; // Make the GET request to download the file
            File.WriteAllBytes(savePath, fileBytes); // Save the downloaded file to the specified path
        }
        catch (Exception e)
        {
            throw new ExarotonException($"An error occurred while getting file data: {e.Message}");
        }
    }
    
    /// <summary>
    /// Write file data to the specified server and path. 
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="path"></param>
    /// <param name="fileData"></param>
    /// <param name="folder"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse WriteFileData(string serverId, string path, byte[] fileData)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"{baseUri}/servers/{serverId}/files/data/{path}/"); // Create the PUT request to upload the file
        var content = new ByteArrayContent(fileData); // Create the request content with the file data
        request.Content = content; // Set the request content
        
        var response = sharedClient.SendAsync(request).Result; // Make the POST request to upload the file and get the response
        var responseString = response.Content.ReadAsStringAsync().Result; // Read the response content as a string
        var writeFileResponse = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return writeFileResponse!;
    }
    
    /// <summary>
    /// Write file data to the specified server and path. 
    /// </summary>
    /// <param name="server"></param>
    /// <param name="path"></param>
    /// <param name="fileData"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse WriteFileData(Server server, string path, byte[] fileData)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"{baseUri}/servers/{server.Id}/files/data/{path}/"); // Create the PUT request to upload the file
        var content = new ByteArrayContent(fileData); // Create the request content with the file data
        request.Content = content; // Set the request content
        
        var response = sharedClient.SendAsync(request).Result; // Make the POST request to upload the file and get the response
        var responseString = response.Content.ReadAsStringAsync().Result; // Read the response content as a string
        var writeFileResponse = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return writeFileResponse!;
    }
    
    /// <summary>
    /// Write a folder to the specified server and path.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="path"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse WriteFolder(string serverId, string path)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"{baseUri}/servers/{serverId}/files/data/{path}/"); // Create the PUT request to upload the folder
        var content = new ByteArrayContent(Array.Empty<byte>()); // Create empty content for folder upload
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("inode/directory"); // Set content type to application/zip
        request.Content = content; // Set the request content
        
        var response = sharedClient.SendAsync(request).Result; // Make the POST request to upload the folder and get the response
        var responseString = response.Content.ReadAsStringAsync().Result; // Read the response content as a string
        var writeFolderResponse = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return writeFolderResponse!;
    }
    
    /// <summary>
    /// Write a folder to the specified server and path.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="path"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse WriteFolder(Server server, string path)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"{baseUri}/servers/{server.Id}/files/data/{path}/"); // Create the PUT request to upload the folder
        var content = new ByteArrayContent(Array.Empty<byte>()); // Create empty content for folder upload
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("inode/directory"); // Set content type to application/zip
        request.Content = content; // Set the request content
        
        var response = sharedClient.SendAsync(request).Result; // Make the POST request to upload the folder and get the response
        var responseString = response.Content.ReadAsStringAsync().Result; // Read the response content as a string
        var writeFolderResponse = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return writeFolderResponse!;
    }
    
    /// <summary>
    /// Delete a file from the specified server and path.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="path"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse DeleteFile(string serverId, string path)
    {
        var content = new StringContent(""); // Create empty content for DELETE request
        string responseString = DeleteRequest($"servers/{serverId}/files/data/{path}/", content).Result; // Make the DELETE request to delete the file
        var deleteFileResponse = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return deleteFileResponse!;
    }
    
    /// <summary>
    /// Delete a file from the specified server and path.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="path"></param>
    /// <returns>GenericResponse</returns>
    public GenericResponse DeleteFile(Server server, string path)
    {
        var content = new StringContent(""); // Create empty content for DELETE request
        string responseString = DeleteRequest($"servers/{server.Id}/files/data/{path}/", content).Result; // Make the DELETE request to delete the file
        var deleteFileResponse = JsonSerializer.Deserialize<GenericResponse>(responseString, jsonOptions); // Deserialize the response into a GenericResponse object
        return deleteFileResponse!;
    }
    
    // Config file methods
    
    /// <summary>
    /// Get configuration options for the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public ConfigOptionsResponse GetConfigOptions(string serverId, string path)
    {
        var content = GetRequest($"servers/{serverId}/files/config/{path}/").Result; // Make the GET request to fetch config options
        var configOptions = JsonSerializer.Deserialize<ConfigOptionsResponse>(content, jsonOptions); // Deserialize the response into a ConfigOptionsResponse object
        return configOptions!;
    }
    
    /// <summary>
    /// Get configuration options for the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="path"></param>
    /// <returns>ConfigOptionsResponse</returns>
    public ConfigOptionsResponse GetConfigOptions(Server server, string path)
    {
        var content = GetRequest($"servers/{server.Id}/files/config/{path}/").Result; // Make the GET request to fetch config options
        var configOptions = JsonSerializer.Deserialize<ConfigOptionsResponse>(content, jsonOptions); // Deserialize the response into a ConfigOptionsResponse object
        return configOptions!;
    }
    
    /// <summary>
    /// Update configuration options for the specified server.
    /// </summary>
    /// <param name="serverId"></param>
    /// <param name="path"></param>
    /// <param name="configOptions"></param>
    /// <returns>ConfigOptionsResponse</returns>
    public ConfigOptionsResponse UpdateConfigOptions(string serverId, string path, Dictionary<string, object> configOptions)
    {
        var content = new StringContent(JsonSerializer.Serialize(configOptions), System.Text.Encoding.UTF8,
            "application/json"); // Create the request content
        var response = PostRequest($"servers/{serverId}/files/config/{path}/", content).Result; // Make the POST request to update config options
        var updatedConfigOptions = JsonSerializer.Deserialize<ConfigOptionsResponse>(response, jsonOptions); // Deserialize the response into a ConfigOptionsResponse object
        return updatedConfigOptions!;
    }
    
    /// <summary>
    /// Update configuration options for the specified server.
    /// </summary>
    /// <param name="server"></param>
    /// <param name="path"></param>
    /// <param name="configOptions"></param>
    /// <returns>ConfigOptionsResponse</returns>
    public ConfigOptionsResponse UpdateConfigOptions(Server server, string path, Dictionary<string, object> configOptions)
    {
        var content = new StringContent(JsonSerializer.Serialize(configOptions), System.Text.Encoding.UTF8, "application/json");
        Console.WriteLine(content.ReadAsStringAsync().Result); // Create the request content
        var response = PostRequest($"servers/{server.Id}/files/config/{path}/", content).Result; // Make the POST request to update config options
        var updatedConfigOptions = JsonSerializer.Deserialize<ConfigOptionsResponse>(response, jsonOptions); // Deserialize the response into a ConfigOptionsResponse object
        return updatedConfigOptions!;
    }
    
    // Credit pool methods
    
    /// <summary>
    /// Get a list of credit pools.
    /// </summary>
    /// <returns>CreditPoolsResponse</returns>
    public CreditPoolsResponse GetCreditPools()
    {
        string responseString = GetRequest("billing/pools/").Result; // Make the GET request to fetch credit pools
        var creditPools = JsonSerializer.Deserialize<CreditPoolsResponse>(responseString, jsonOptions); // Deserialize the response into a CreditPoolsResponse object
        return creditPools!;
    }
    
    /// <summary>
    /// Get information about a specified credit pool.
    /// </summary>
    /// <param name="poolId"></param>
    /// <returns>CreditPoolResponse</returns>
    public CreditPoolResponse GetCreditPool(string poolId)
    {
        string responseString = GetRequest($"billing/pools/{poolId}/").Result; // Make the GET request to fetch the credit pool
        var creditPool = JsonSerializer.Deserialize<CreditPoolResponse>(responseString, jsonOptions); // Deserialize the response into a CreditPoolResponse object
        return creditPool!;
    }
    
    /// <summary>
    /// Get information about a specified credit pool.
    /// </summary>
    /// <param name="pool"></param>
    /// <returns>CreditPoolResponse</returns>
    public CreditPoolResponse GetCreditPool(CreditPool pool)
    {
        string responseString = GetRequest($"billing/pools/{pool.Id}/").Result; // Make the GET request to fetch the credit pool
        var creditPool = JsonSerializer.Deserialize<CreditPoolResponse>(responseString, jsonOptions); // Deserialize the response into a CreditPoolResponse object
        return creditPool!;
    }
    
    /// <summary>
    /// Get credit pool members for a specified pool.
    /// </summary>
    /// <param name="poolId"></param>
    /// <returns>CreditPoolMembersResponse</returns>
    public CreditPoolMembersResponse GetCreditPoolMembers(string poolId)
    {
        string responseString = GetRequest($"billing/pools/{poolId}/members/").Result; // Make the GET request to fetch credit pool members
        var creditPoolMembers = JsonSerializer.Deserialize<CreditPoolMembersResponse>(responseString, jsonOptions); // Deserialize the response into a CreditPoolMembersResponse object
        return creditPoolMembers!;
    }
    
    /// <summary>
    /// Get credit pool members for a specified pool.
    /// </summary>
    /// <param name="pool"></param>
    /// <returns>CreditPoolMembersResponse</returns>
    public CreditPoolMembersResponse GetCreditPoolMembers(CreditPool pool)
    {
        string responseString = GetRequest($"billing/pools/{pool.Id}/members/").Result; // Make the GET request to fetch credit pool members
        var creditPoolMembers = JsonSerializer.Deserialize<CreditPoolMembersResponse>(responseString, jsonOptions); // Deserialize the response into a CreditPoolMembersResponse object
        return creditPoolMembers!;
    }

    /// <summary>
    /// Get credit pool servers for a specified pool.
    /// </summary>
    /// <param name="poolId"></param>
    /// <returns>CreditPoolServersResponse</returns>
    public CreditPoolServersResponse GetCreditPoolServers(string poolId)
    {
        string responseString = GetRequest($"billing/pools/{poolId}/servers/").Result; // Make the GET request to fetch credit pool servers
        var creditPoolServers = JsonSerializer.Deserialize<CreditPoolServersResponse>(responseString, jsonOptions); // Deserialize the response into a CreditPoolServersResponse object
        return creditPoolServers!;
    }
    
    /// <summary>
    /// Get credit pool servers for a specified pool.
    /// </summary>
    /// <param name="pool"></param>
    /// <returns>CreditPoolServersResponse</returns>
    public CreditPoolServersResponse GetCreditPoolServers(CreditPool pool)
    {
        string responseString = GetRequest($"billing/pools/{pool.Id}/servers/").Result; // Make the GET request to fetch credit pool servers
        var creditPoolServers = JsonSerializer.Deserialize<CreditPoolServersResponse>(responseString, jsonOptions); // Deserialize the response into a CreditPoolServersResponse object
        return creditPoolServers!;
    }
}