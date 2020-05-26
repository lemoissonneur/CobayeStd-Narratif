using System;

public interface IPlayerInput
{
    Action<string> OnAcceptedStringInput { get; set; }
    string TextInput { get; }
}