/*
 * UITimeline.cs
 * Created by: Newgame+ LD
 * Created on: 6/5/2020 (dd/mm/yy)
 */

using System;
using UnityEngine;
using UnityEngine.UI;

public class UITimeline : MonoBehaviour {
  [SerializeField] RectTransform playhead = null;
  [SerializeField] Text debugTime = null;
  [SerializeField] AnimationCurve sunOpacity = null;
  [SerializeField] Image[] sunPics = null;
  [SerializeField] float[] opDiv = null; //Opacity division in case you're wondering.
  [SerializeField] AnimationCurve moonOpacity = null;
  [SerializeField] Image moonPic = null;

  void Update () {
    if (GameManager._IsPaused || GameManager._Player._Paralyzed)
    {
      return;
    }

    float currentTime = GameManager._DayController._CurrentTime;

    for (int i = 0; i < sunPics.Length; i++) {
      sunPics[i].color = new Color (sunPics[i].color.r, sunPics[i].color.g, sunPics[i].color.b, sunOpacity.Evaluate (currentTime % 1) * opDiv[i]);
    }

    moonPic.color = new Color (1, 1, 1, moonOpacity.Evaluate (currentTime % 1));
    playhead.anchorMin = playhead.anchorMax = new Vector2 (currentTime * 2 % 1, 0.5f);

    if (debugTime) {
      TimeSpan td = TimeSpan.FromSeconds (((GameManager._DayController._CurrentTime + 0.25) % 1) * 1440);
      debugTime.text = string.Format ("{0:0}:{1:00}", td.Minutes, td.Seconds);
    }
  }
}
