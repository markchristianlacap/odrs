namespace Backend.Services;

public interface IStorageService
{
    Task<string> UploadFileAsync(
        byte[] file,
        string? subDirectory = null,
        CancellationToken ct = default
    );
    Task<string> UploadFileAsync(
        IFormFile file,
        string? subDirectory = null,
        CancellationToken ct = default
    );
    Task<string> UploadFileAsync(
        Stream file,
        string? subDirectory = null,
        CancellationToken ct = default
    );
    Task<byte[]> DownloadFileAsync(string path, CancellationToken ct = default);
    Task RemoveFileAsync(string path, CancellationToken ct = default);
}

public class StorageService(IConfiguration config) : IStorageService
{
    private readonly string _rootDirectory = config.GetValue<string>("Directory") ?? "";

    public Task<byte[]> DownloadFileAsync(string path, CancellationToken ct = default)
    {
        var filePath = Path.Combine(_rootDirectory, path);
        return File.ReadAllBytesAsync(filePath, ct);
    }

    public async Task RemoveFileAsync(string path, CancellationToken ct = default)
    {
        var filePath = Path.Combine(_rootDirectory, path);
        File.Delete(filePath);
        await Task.CompletedTask;
    }

    public async Task<string> UploadFileAsync(
        byte[] file,
        string? subDirectory = null,
        CancellationToken ct = default
    )
    {
        //store byte to file
        var path = Path.Combine(subDirectory ?? "", Path.GetRandomFileName());
        if (!string.IsNullOrEmpty(subDirectory))
        {
            Directory.CreateDirectory(Path.Combine(_rootDirectory, subDirectory));
        }
        var filePath = Path.Combine(_rootDirectory, path);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await stream.WriteAsync(file, ct);
        }
        return path;
    }

    public async Task<string> UploadFileAsync(
        Stream file,
        string? subDirectory = null,
        CancellationToken ct = default
    )
    {
        var path = Path.Combine(subDirectory ?? "", Path.GetRandomFileName());
        if (!string.IsNullOrEmpty(subDirectory))
        {
            Directory.CreateDirectory(Path.Combine(_rootDirectory, subDirectory));
        }
        var filePath = Path.Combine(_rootDirectory, path);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream, ct);
        }
        return path;
    }

    public async Task<string> UploadFileAsync(
        IFormFile file,
        string? subDirectory = null,
        CancellationToken ct = default
    )
    {
        var path = Path.Combine(subDirectory ?? "", Path.GetRandomFileName());
        if (!string.IsNullOrEmpty(subDirectory))
        {
            Directory.CreateDirectory(Path.Combine(_rootDirectory, subDirectory));
        }
        var filePath = Path.Combine(_rootDirectory, path);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream, ct);
        }
        return path;
    }
}
