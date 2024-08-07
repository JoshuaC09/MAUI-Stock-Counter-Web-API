﻿namespace WebApplication2.Models
{
    public class AppSettingSecurity
    {
        public const string EncryptionKey = "In the eye of the beholder doth lie beauty's true essence, for each gaze doth fashion its own fair visage";
        public const string EncryptionSalt = "The only way to guarantee peace is by making the prospect of war seem hopeless";

        public string? DecryptedJwtKey { get; set; }
    }
}
