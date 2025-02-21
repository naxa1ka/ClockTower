﻿using Nxlk.UIToolkit;
using UnityEngine.UIElements;

namespace BloodClockTower.Game
{
    public class EditPlayerView : IEditPlayerView
    {
        public Button StartEditingButton { get; private set; }
        public Button EndEditingButton { get; private set; }
        public Button OpenPlayerNotesButton { get; private set; }
        public Button KillPlayerButton { get; private set; }
        public TextField NameInputField { get; private set; }

        public EditPlayerView(ISafetyUiDocument safetyUiDocument)
        {
            StartEditingButton = safetyUiDocument.Q<Button>("edit-button");
            EndEditingButton = safetyUiDocument.Q<Button>("end-editing-button");
            OpenPlayerNotesButton = safetyUiDocument.Q<Button>("open-player-notes-button");
            KillPlayerButton = safetyUiDocument.Q<Button>("kill-player-button");
            NameInputField = safetyUiDocument.Q<TextField>("name-input-field");
        }
    }
}
