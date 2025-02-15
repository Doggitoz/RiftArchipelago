using System;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using UnityEngine;

namespace RiftArchipelago;

public enum APState {
    Menu,
    InGame,
}

public static class ArchipelagoClient {
    public static int[] AP_VERSION = [0, 5, 0];
    public const string GAME_NAME = "Clique";
    public static bool isAuthenticated = false;
    public static ArchipelagoInfo apInfo = new ArchipelagoInfo();
    public static ArchipelagoUI apUI = new ArchipelagoUI();
    public static APState state;
    private static GameObject obj;
    public static int slotID;
    public static ArchipelagoSession session;
    public static SlotData slotData;
    public static bool isInGame = false;

    // public static void Setup(ManualLogSource log) {
    //     _log = log;
    //     obj = new();
    //     obj.name = "ArchipelagoClient";
    //     DontDestroyOnLoad(obj);
    //     AP = obj.AddComponent<ArchipelagoClient>();
    // }

    public static bool Connect() {
        if (isAuthenticated) {
            return true;
        }

        if (apInfo.address is null || apInfo.address.Length == 0) {
            return false;
        }

        session = ArchipelagoSessionFactory.CreateSession(apInfo.address);
        
        LoginResult loginResult = session.TryConnectAndLogin(
            GAME_NAME,
            apInfo.slot,
            ItemsHandlingFlags.AllItems,
            new Version(AP_VERSION[0], AP_VERSION[1], AP_VERSION[2]),
            null,
            "",
            apInfo.password
        );

        if (loginResult is LoginSuccessful loginSuccess) {
            isAuthenticated = true;
            state = APState.InGame;
            slotData = new SlotData(loginSuccess.SlotData);
            return true;
        }

        return false;
    }

    public static async void Disconnect() {
        if(session is { Socket.Connected: true }) await session.Socket.DisconnectAsync(); 
    }
    
}