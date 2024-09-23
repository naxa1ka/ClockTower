﻿using Nxlk.ReactiveUIToolkit;
using Nxlk.UIToolkit;
using Nxlk.UniRx;
using UniRx;
using UnityEngine;

namespace BloodClockTower.Game
{
    public class VotingHistoryPresenter : DisposableObject, IPresenter
    {
        private readonly VotingHistoryView _view;
        private readonly VotingHistoryViewModel _viewModel;
        private readonly VotingSystemViewModel _votingSystemViewModel;
        private readonly EditPlayerViewModel _editPlayerViewModel;

        public VotingHistoryPresenter(
            VotingHistoryView view,
            VotingHistoryViewModel viewModel,
            VotingSystemViewModel votingSystemViewModel,
            EditPlayerViewModel editPlayerViewModel
        )
        {
            _view = view;
            _viewModel = viewModel;
            _votingSystemViewModel = votingSystemViewModel;
            _editPlayerViewModel = editPlayerViewModel;
        }

        public void Initialize()
        {
            _view.OpenVotingHistoryButton.Hide();
            Observable
                .CombineLatest(
                    _viewModel.VotingRounds.ObserveCountChangedWithCount(),
                    _votingSystemViewModel.CurrentState,
                    _editPlayerViewModel.IsEditing,
                    (votingRoundsCount, state, isEditingPlayer) =>
                        votingRoundsCount > 0
                        && state == VotingSystemViewModel.State.Idle
                        && !isEditingPlayer
                )
                .BindToVisible(_view.OpenVotingHistoryButton)
                .AddTo(disposables);
            _viewModel.IsVisible.BindToVisible(_view.VotingHistoryContainer).AddTo(disposables);
            _view.OpenVotingHistoryButton.SubscribeOnClick(_viewModel.Show).AddTo(disposables);
            _view.CloseVotingHistoryButton.SubscribeOnClick(_viewModel.Hide).AddTo(disposables);
            EscapeObservable.Instance
                .Where(_ => _viewModel.IsVisible.Value)
                .Subscribe(_viewModel.Hide)
                .AddTo(disposables);
            _viewModel
                .IsVisible.WhereTrue()
                .Subscribe(() => _view.VotingHistoryLabel.text = _viewModel.VotingRounds.ToString())
                .AddTo(disposables);
            _view.NoteInputField.ObserveBlur().Subscribe(_viewModel.EndEditingNote).AddTo(disposables);
            _view.NoteInputField.ObserveText().Subscribe(_viewModel.ChangeNote).AddTo(disposables);
            _viewModel
                .VotingRounds.Note.Subscribe(_view.NoteInputField.SetValueWithoutNotify)
                .AddTo(disposables);
        }
    }
}
