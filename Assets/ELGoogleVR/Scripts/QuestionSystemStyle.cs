using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionSystemStyle : ScriptableObject
{
    public enum LayoutDirection { RTL, LTR }

    public LayoutDirection layoutDirection;

    public TMP_FontAsset font;
    public Sprite background;

    public Color colorStartMessage;
    public Color colorStartAction;
    public Color colorQuestion;
    public Color colorChoice;
    public Color colorReward;
    public Color colorFinish;

    public Sprite startActionSprite;
    public Sprite rewardBackground;

    // 3 expected sprites [0 - normal] [1 - correct] [2 - wrong]
    public Sprite[] trueSprites;
    public Sprite[] falseSprites;
    public Sprite[] choiceBackgroundSprites;

    public TextAudioBlock startMessageTAblock;
    public TextAudioBlock startActionTAblock;
    public TextAudioBlock finishMessageGoodTAblock;
    public TextAudioBlock finishMessageBadTAblock;
    
    public AudioClip[] correctAnswerFeedbacks;
    public AudioClip[] wrongAnswerFeedbacks;
    public AudioClip choiceTrue;
    public AudioClip choiceFalse;
}
