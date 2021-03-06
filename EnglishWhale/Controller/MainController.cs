using EnglishWhale.Models;
using EnglishWhale.Services;
using EnglishWhale.Services.DownloadService;
using EnglishWhale.Services.DownloadService.Implementation;
using EnglishWhale.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using WMPLib;

namespace EnglishWhale.Controller
{
    public class MainController
    {
        //must be even
        private const int LEARNING_WORD_PAIRS_NUMBER = 4;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private string tempPathForAudio;
        private LanguageDictionary currentDictionary;
        private WindowsMediaPlayer wplayer;
        private bool isTimerNeeded;
        private Random rnd;
        private List<LanguageDictionary> vocabularies;
        IDownloader downloader;
        public MainController()
        {
            tempPathForAudio = Path.Combine(Path.GetTempPath(), @"audio");
            downloader = new DownloaderBufferedProxy();
            rnd = new Random((int)DateTime.Now.ToBinary());
        }

        public string ChooseCSVFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "(*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            else
            {
                return null;
            }
        }

        public void OpenQuizzesChooser(string path, MainForm mForm)
        {
            CsvReader csvReader;
            if (File.Exists(path))
            {
                try
                {
                    csvReader = new CsvReader(path);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "Can not open 'Quizzes Chooser'.");
                    MessageBox.Show(mForm, ex.Message + " Please fix the issue and try again.", "File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                QuizzesChooserForm qcForm = new QuizzesChooserForm(this);
                vocabularies = csvReader.Vocabularies;
                qcForm.Add(csvReader.Vocabularies);
                qcForm.FormClosed += 
                    delegate 
                    {
                        try
                        {
                            if (vocabularies[0].IsEnglishTo)
                            {
                                CsvWritter.writeCsv(vocabularies[0], path);
                            }
                            else
                            {
                                CsvWritter.writeCsv(vocabularies[1], path);
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(mForm, "Selected .csv file opened by another program.", "File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        mForm.Visible = true; 
                    };
                mForm.Visible = false;
                qcForm.Show();
            }
            else
            {
                MessageBox.Show(mForm, "Selected .csv file not found.", "File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void StartLearning(LanguageDictionary languageDictionary, Form parentForm)
        {
            foreach (LanguageDictionary langDic in vocabularies)
            {
                if (langDic.IsEnglishTo)
                {
                    currentDictionary = langDic;
                    break;
                }
            }
            LearningForm lForm = new LearningForm(this);
            lForm.FormClosing += delegate { parentForm.Visible = true; };
            parentForm.Visible = false;
            lForm.Show();
        }

        public void StartWrittenQuiz(LanguageDictionary languageDictionary, Form parentForm)
        {
            parentForm.Visible = false;
            currentDictionary = languageDictionary;
            WrittenQuizForm form = new WrittenQuizForm(this);
            form.FormClosing += delegate { parentForm.Visible = true; };
            SetMutes(form);
            form.Show();
        }

        public void SetMutes(IMute form)
        {
            form.MuteAnswer = currentDictionary.IsEnglishFrom;
            form.MuteQuestion = currentDictionary.IsEnglishTo;
        }

        public void SetMutesForLearningOnly(IMute form)
        {
            form.MuteAnswer = currentDictionary.IsEnglishTo;
            form.MuteQuestion = currentDictionary.IsEnglishFrom;
        }

        public WordsPair GetLearnedRandomWordsPair()
        {
            if (currentDictionary.LearnedWords.Count != 0)
            {
                return GetRandomWordsPair(currentDictionary.LearnedWords);
            }
            if (currentDictionary.WordsToStudy.Count != 0)
            {
                return GetRandomWordsPair(currentDictionary.WordsToStudy);
            }
            return null;
        }

        public WordsPair GetRandomWordsPairToStudy()
        {
            if (currentDictionary.WordsToStudy.Count != 0)
            {
                return GetRandomWordsPair(currentDictionary.WordsToStudy);
            }
            if (currentDictionary.LearnedWords.Count != 0)
            {
                return GetRandomWordsPair(currentDictionary.LearnedWords);
            }
            return null;
        }

        public WordsPair GetAnyRandomWordsPair()
        {
            List<WordsPair> studyList = currentDictionary.WordsToStudy;
            List<WordsPair> learnedList = currentDictionary.LearnedWords;
            int rundomNumber = rnd.Next(0, studyList.Count + learnedList.Count);
            List<WordsPair> list = rundomNumber < studyList.Count ? currentDictionary.WordsToStudy : currentDictionary.LearnedWords;
            
            return GetRandomWordsPair(list);
        }

        public WordsPair GetRandomWordsPair(List<WordsPair> list)
        {
            int testPairNumber = rnd.Next(0, list.Count);
            WordsPair pair = list.ElementAt(testPairNumber);
            return pair;
        }

        public void StartChooseAnswerQuiz(LanguageDictionary languageDictionary, bool timer, Form parentForm)
        {
            isTimerNeeded = timer;
            currentDictionary = languageDictionary;
            ChooseAnswerQuizForm qcForm = new ChooseAnswerQuizForm(this, timer);
            qcForm.FormClosing += delegate { parentForm.Visible = true; };
            parentForm.Visible = false;

            qcForm.Show();
        }

        public bool isRightAnswer(string rightAnswer, string userAnswer)
        {
            rightAnswer = Regex.Replace(rightAnswer.ToLower(), @"\.|,|\(.*?\)|^to\s|^a\s|\?|\!", "").Replace("  ", " ");
            userAnswer = Regex.Replace(userAnswer.ToLower(), @"\.|,|\(.*?\)|^to\s|^a\s|\?|\!", "").Replace("  ", " ");
            string[] answers = rightAnswer.Split(';');
            foreach (string rightAns in answers)
            {
                if (rightAns.Trim().Equals(userAnswer.Trim()))
                {
                    return true;
                }
            }
            return false;
        }

        public void SpeakThis(string phrase)
        {
            string voicePath;
            try
            {
                voicePath = downloader.DownloadVoice(phrase, tempPathForAudio);
            }
            catch (WebException ex)
            {
                Logger.Error(ex, "Unable to download voice.");
                return;
            }

            WaitingStopPlayer();
            PlayVoiceFile(voicePath);
        }
        private void PlayVoiceFile(string voicePath)
        {
            wplayer = new WindowsMediaPlayer();

            wplayer.URL = voicePath;
            IWMPControls3 controls = (IWMPControls3)wplayer.controls;
            controls.play();

            wplayer.PlayStateChange += new _WMPOCXEvents_PlayStateChangeEventHandler(Wplayer_StatusChange);
            wplayer.MediaError += new _WMPOCXEvents_MediaErrorEventHandler(Player_MediaError);
        }
        private void WaitingStopPlayer()
        {
            IWMPControls3 controls;
            if (wplayer != null)
            {
                do
                {
                    controls = (IWMPControls3)wplayer.controls;
                    double lastPosition = controls.currentPosition;
                    Thread.Sleep(100);
                    bool payngPositionChanged = (controls.currentPosition - lastPosition) > 0.001;
                    if (!payngPositionChanged)
                    {
                        if (controls.get_isAvailable("stop"))
                        {
                            controls.stop();
                        }
                    }
                } while (wplayer.playState == WMPPlayState.wmppsPlaying);

            }
        }

        public QuizWithAnswers GetNewChooseAnswerQuiz()
        {
            WordsPair testPair = GetAnyRandomWordsPair();
            string question = testPair.Original;
            string rightAnswer = testPair.Translation;
            string[] wrongs = new string[3];
            for (int i = 0; i < 3; i++)
            {
                WordsPair wrongPair;
                do
                {
                    wrongPair = GetAnyRandomWordsPair();
                } while (wrongPair.Equals(testPair));

                wrongs[i] = wrongPair.Translation;
            }
            QuizWithAnswers quiz = new QuizWithAnswers(question, rightAnswer, wrongs[0], wrongs[1], wrongs[2]);
            return quiz;
        }

        private void Wplayer_StatusChange(int newState)
        {
            if (newState != (int)WMPPlayState.wmppsPlaying)
            {
                IWMPPlaylist playList = wplayer.mediaCollection.getAll();
                if (playList.count >= 1) 
                {
                    IWMPMedia3 media = (IWMPMedia3)playList.get_Item(0);
                    wplayer.mediaCollection.remove(media, true);
                }
                wplayer.close();
            }
        }
        private void Player_MediaError(object pMediaObject)
        {
            MessageBox.Show("Cannot play media file.");
            wplayer.close();
        }

        public LearningService GetLearningService()
        {
            HashSet<WordsPair> learningSet = new HashSet<WordsPair>(LEARNING_WORD_PAIRS_NUMBER);
            for (int i = 0; i < LEARNING_WORD_PAIRS_NUMBER; i++)
            {
                int protector = 0;
                WordsPair pair;
                do
                {
                    pair = GetRandomWordsPairToStudy();
                }
                while (!learningSet.Add(pair) && ++protector < 40);
            }
            return new LearningService(learningSet.ToList());
        }
    }
}
