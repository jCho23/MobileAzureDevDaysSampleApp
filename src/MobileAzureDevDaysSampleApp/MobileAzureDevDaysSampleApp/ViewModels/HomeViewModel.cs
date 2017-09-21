﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using MobileAzureDevDaysSampleApp.Services;
using MobileAzureDevDaysSampleApp.Constants;

namespace MobileAzureDevDaysSampleApp.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        string emojiLabelText = string.Empty;
        string userInputEditorText = string.Empty;
        bool isInternetConnectionActive;
        ICommand submitButtonCommand;
        Color backgroundColor = ColorConstants.DefaultBackgroundColor;

        public event EventHandler<string> SentimentAnalyisFailed;
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsInternetConnectionInactive => !IsInternetConnectionActive;

        public ICommand SubmitButtonCommand => submitButtonCommand ??
            (submitButtonCommand = new Command(async () => await ExecuteSubmitButtonCommand()));

        public bool IsInternetConnectionActive
        {
            get => isInternetConnectionActive;
            set => SetProperty(ref isInternetConnectionActive, value, () => OnPropertyChanged(nameof(IsInternetConnectionInactive)));
        }

        public string EmojiLabelText
        {
            get => emojiLabelText;
            set => SetProperty(ref emojiLabelText, value);
        }

        public string UserInputEditorText
        {
            get => userInputEditorText;
            set => SetProperty(ref userInputEditorText, value);
        }

        public Color BackgroundColor
        {
            get => backgroundColor;
            set => SetProperty(ref backgroundColor, value);
        }

        async Task ExecuteSubmitButtonCommand()
        {
            SetIsBusy(true);

            try
            {
                var result = await TextAnalysis.GetSentiment(UserInputEditorText);
                if (result == null)
                {
                    OnSentimentAnalyisFailed("No Results Returned");
                }
                else
                {
                    SetBackgroundColor((float)result);
                    SetEmoji((float)result);
                }

            }
            catch (Exception e)
            {
                OnSentimentAnalyisFailed(e.Message);
            }
            finally
            {
                SetIsBusy(false);
            }
        }

        void SetEmoji(float result)
        {
            switch (result)
            {
                case float number when (number < 0.4):
                    EmojiLabelText = EmojiConstants.SadFaceEmoji;
                    break;
                case float number when (number >= 0.4 && number <= 0.6):
                    EmojiLabelText = EmojiConstants.HappyFaceEMoji;
                    break;
                case float number when (number > 0.6):
                    EmojiLabelText = EmojiConstants.NeutralFaceEMoji;
                    break;
            }
        }

        void SetBackgroundColor(float result)
        {
            switch (result)
            {
                case float number when (number <= 0.1):
                    BackgroundColor = ColorConstants.EmotionColor1;
                    break;
                case float number when (number > 0.1 && number <= 0.2):
                    BackgroundColor = ColorConstants.EmotionColor2;
                    break;
                case float number when (number > 0.2 && number <= 0.3):
                    BackgroundColor = ColorConstants.EmotionColor3;
                    break;
                case float number when (number > 0.3 && number <= 0.6):
                    BackgroundColor = ColorConstants.EmotionColor4;
                    break;
                case float number when (number > 0.6 && number <= 0.7):
                    BackgroundColor = ColorConstants.EmotionColor5;
                    break;
                case float number when (number > 0.7 && number <= 0.8):
                    BackgroundColor = ColorConstants.EmotionColor6;
                    break;
                case float number when (number > 0.8 && number <= 0.9):
                    BackgroundColor = ColorConstants.EmotionColor7;
                    break;
                case float number when (number > 0.9):
                    BackgroundColor = ColorConstants.EmotionColor8;
                    break;
            }
        }

        void SetIsBusy(bool isBusy)
        {
            switch (isBusy)
            {
                case true:
                    BackgroundColor = ColorConstants.DefaultBackgroundColor;
                    EmojiLabelText = string.Empty;
                    break;
            }

            IsInternetConnectionActive = isBusy;
        }

        void SetProperty<T>(ref T backingStore, T value, Action onChanged = null, [CallerMemberName] string propertyname = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            backingStore = value;

            onChanged?.Invoke();

            OnPropertyChanged(propertyname);
        }

        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        void OnSentimentAnalyisFailed(string errorMessage) =>
            SentimentAnalyisFailed?.Invoke(this, errorMessage);
    }
}
