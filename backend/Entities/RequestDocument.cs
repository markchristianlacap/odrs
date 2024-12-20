﻿using Backend.Enums;

namespace Backend.Entities;

public class RequestDocument
{
    public Guid Id { get; set; }
    public Guid RequestId { get; set; }
    public Request Request { get; set; } = null!;
    public DocumentType Type { get; set; }
    public string Purpose { get; set; } = null!;
    public int Copies { get; set; }
}
