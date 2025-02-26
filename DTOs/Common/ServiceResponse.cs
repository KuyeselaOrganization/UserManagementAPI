namespace UserManagementAPI.DTOs.Common;

using System.Collections.Generic;

public class ServiceResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }
}
