﻿namespace Shared.Types;

public sealed class Event
{
    public DateOnly? Date { get; set; }

    public Place Place { get; set; } = new();
}