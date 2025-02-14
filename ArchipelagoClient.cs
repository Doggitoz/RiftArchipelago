using System;
using System.Collections;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using BepInEx.Logging;
using UnityEngine;

namespace RiftArchipelago;

public enum APState {
    Menu,
    InGame,
}

public class ArchipelagoClient : MonoBehaviour {
    public static int[] AP_VERSION = [0, 5, 0];
    public const string GAME_NAME = "Clique";
    private static ManualLogSource _log;
    public static bool isAuthenticated;
    public static ArchipelagoInfo apInfo = new ArchipelagoInfo();
    public static ArchipelagoUI apUI = new ArchipelagoUI();
    public static ArchipelagoClient AP;
    public static APState state;
    private static GameObject obj;
    public int slotID;
    public static ArchipelagoSession session;
    public static SlotData slotData;
    public static bool isInGame = false;

    public static void Setup(ManualLogSource log) {
        _log = log;
        obj = new();
        obj.name = "ArchipelagoClient";
        DontDestroyOnLoad(obj);
        AP = obj.AddComponent<ArchipelagoClient>();
    }

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

        _log.LogInfo(loginResult);

        if (loginResult is LoginSuccessful loginSuccess) {
            isAuthenticated = true;
            state = APState.InGame;
            slotData = new SlotData(loginSuccess.SlotData, _log);
            return true;
        }

        return false;
    }
    
}