// 管理者 菅沼
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace SLib
{
    namespace Tweening
    {
        enum UIEasingMode
        {
            EaseInQuad,
            EaseOutQuad,
            EaseInOutQuad,
            EaseInCubic,
            EaseOutCubic,
            EaseInOutCubic,
            EaseInQuart,
            EaseOutQuart,
            EaseInOutQuart,
            EaseInQuint,
            EaseOutQuint,
            EaseInOutQuint,
            EaseInExpo,
            EaseOutExpo,
            EaseInOutExpo,
            EaseInCirc,
            EaseOutCirc,
            EaseInOutCirc,
            EaseInBack,
            EaseOutBack,
            EaseInOutBack,
            EaseInElastic,
            EaseOutElastic,
            EaseInOutElastic,
            EaseInBounce,
            EaseOutBounce,
            EaseInOutBounce,
        }

        public class UITween : MonoBehaviour
        {
            #region Process Core
            // 移動してくる画像
            [SerializeField,
                Header("イージングさせるImageコンポーネントを含むオブジェクトをアタッチ")]
            Image _movingImage;
            // ゴールの画面座標
            [SerializeField,
                Header("終点")] 
            RectTransform _goalRect;
            // スタートの画面座標
            [SerializeField,
                Header("始点")] 
            RectTransform _startRect;
            // デュレーション
            [SerializeField, 
                Header("イージングにかける時間")] 
            float _duration;
            // スケールまでもTweeningするかのフラグ
            [SerializeField,
                Header("拡大縮小しながらイージングするかのフラグ")]
            bool _easeWithScale;
            // Tweeningが完了したときのイベント
            [SerializeField,
                Header("イージングが終わった際に発火させたいイベント")] 
            UnityEvent _onTweeningEnd;
            // TweeningMode
            [SerializeField,
                Header("イージングモード")] 
            UIEasingMode _mode;
            // シーンに入ったらTweenするのか
            [SerializeField,
                Header("シーン突入時にTweenを開始するのか")]
            bool _startOnAwake;

            // アニメーションしているかのフラグ
            bool _bIsAnimating = false;
            public bool IsTweening => _bIsAnimating;
            float _elapsedTime = 0;

            private void Start()
            {
                if (_startOnAwake) StartTween();
            }

            private void FixedUpdate()
            {
                if (_bIsAnimating)
                {
                    //Debug.Log("Tweening");
                    _elapsedTime += Time.deltaTime / _duration;
                    float t;
                    #region EachMethod
                    t = _mode switch
                    {
                        UIEasingMode.EaseInQuad => EaseInQuad(_elapsedTime),
                        UIEasingMode.EaseOutQuad => EaseOutQuad(_elapsedTime),
                        UIEasingMode.EaseInOutQuad => EaseInOutQuad(_elapsedTime),
                        UIEasingMode.EaseInCubic => EaseInCubic(_elapsedTime),
                        UIEasingMode.EaseOutCubic => EaseOutCubic(_elapsedTime),
                        UIEasingMode.EaseInOutCubic => EaseInOutCubic(_elapsedTime),
                        UIEasingMode.EaseInQuart => EaseInQuart(_elapsedTime),
                        UIEasingMode.EaseOutQuart => EaseOutQuart(_elapsedTime),
                        UIEasingMode.EaseInOutQuart => EaseInOutQuart(_elapsedTime),
                        UIEasingMode.EaseInQuint => EaseInQuint(_elapsedTime),
                        UIEasingMode.EaseOutQuint => EaseOutQuint(_elapsedTime),
                        UIEasingMode.EaseInOutQuint => EaseInOutQuint(_elapsedTime),
                        UIEasingMode.EaseInExpo => EaseInExpo(_elapsedTime),
                        UIEasingMode.EaseOutExpo => EaseOutExpo(_elapsedTime),
                        UIEasingMode.EaseInOutExpo => EaseInOutExpo(_elapsedTime),
                        UIEasingMode.EaseInCirc => EaseInCirc(_elapsedTime),
                        UIEasingMode.EaseOutCirc => EaseOutCirc(_elapsedTime),
                        UIEasingMode.EaseInOutCirc => EaseInOutCirc(_elapsedTime),
                        UIEasingMode.EaseInBack => EaseInBack(_elapsedTime),
                        UIEasingMode.EaseOutBack => EaseOutBack(_elapsedTime),
                        UIEasingMode.EaseInOutBack => EaseInOutBack(_elapsedTime),
                        UIEasingMode.EaseInElastic => EaseInElastic(_elapsedTime),
                        UIEasingMode.EaseOutElastic => EaseOutElastic(_elapsedTime),
                        UIEasingMode.EaseInOutElastic => EaseInOutElastic(_elapsedTime),
                        UIEasingMode.EaseInBounce => EaseInBounce(_elapsedTime),
                        UIEasingMode.EaseOutBounce => EaseOutBounce(_elapsedTime),
                        UIEasingMode.EaseInOutBounce => EaseInOutBounce(_elapsedTime),
                        _ => EaseInBack(_elapsedTime)
                    };
                    #endregion

                    float xPos = (1 - t) * _startRect.position.x + t * _goalRect.position.x;
                    float yPos = (1 - t) * _startRect.position.y + t * _goalRect.position.y;
                    _movingImage.rectTransform.position = new Vector3(xPos, yPos, 0);

                    if (_easeWithScale)
                    {
                        float xScl = (1 - t) * _startRect.localScale.x + t * _goalRect.localScale.x;
                        float yScl = (1 - t) * _startRect.localScale.y + t * _goalRect.localScale.y;
                        _movingImage.rectTransform.localScale = new Vector3(xScl, yScl, 0);
                    }

                    if (_elapsedTime > 1f)
                    {
                        _bIsAnimating = false;
                        _movingImage.rectTransform.position = _goalRect.position;
                        _onTweeningEnd?.Invoke();
                    }
                }
            }

            public void StartTween()
            {
                if (_bIsAnimating) { return; }
                else
                {
                    _bIsAnimating = true;
                }
            }

            public void ResetUIElementsPosition()
            {
                _movingImage.rectTransform.position = _startRect.position;
            }

            #endregion
            #region EasingFormulas
            float EaseInSine(float t)
            {
                return 1 - Mathf.Cos((t * Mathf.PI) / 2.0f);
            }
            float EaseOutSine(float t)
            {
                return Mathf.Sin((t * Mathf.PI) / 2.0f);
            }
            float EaseInOutSine(float t)
            {
                return -(Mathf.Cos(Mathf.PI * t) - 1) / 2;
            }

            float EaseInQuad(float t)
            {
                return t * t;
            }
            float EaseOutQuad(float t)
            {
                return 1 - (1 - t) * (1 - t);
            }
            float EaseInOutQuad(float t)
            {
                return t < 0.5f ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
            }

            float EaseInCubic(float t)
            {
                return t * t * t;
            }
            float EaseOutCubic(float t)
            {
                return 1 - Mathf.Pow(1 - t, 3);
            }
            float EaseInOutCubic(float t)
            {
                return t < 0.5f ? 4 * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
            }

            float EaseInQuart(float t)
            {
                return t * t * t * t;
            }
            float EaseOutQuart(float t)
            {
                return 1 - Mathf.Pow(1 - t, 4);
            }
            float EaseInOutQuart(float t)
            {
                return t < 0.5f ? 8 * t * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 4) / 2;
            }

            float EaseInQuint(float t)
            {
                return t * t * t * t * t;
            }
            float EaseOutQuint(float t)
            {
                return 1 - Mathf.Pow(1 - t, 5);
            }
            float EaseInOutQuint(float t)
            {
                return t < 0.5f ? 16 * t * t * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 5) / 2;
            }

            float EaseInExpo(float t)
            {
                return t == 0 ? 0 : Mathf.Pow(2, 10 * t - 10);
            }
            float EaseOutExpo(float t)
            {
                return t == 1 ? 1 : 1 - Mathf.Pow(2, -10 * t);
            }
            float EaseInOutExpo(float t)
            {
                return t == 0
                  ? 0
                  : t == 1
                  ? 1
                  : t < 0.5f ? Mathf.Pow(2, 20 * t - 10) / 2
                  : (2 - Mathf.Pow(2, -20 * t + 10)) / 2;
            }

            float EaseInCirc(float t)
            {
                return 1 - Mathf.Sqrt(1 - Mathf.Pow(t, 2));
            }
            float EaseOutCirc(float t)
            {
                return Mathf.Sqrt(1 - Mathf.Pow(t - 1, 2));
            }
            float EaseInOutCirc(float t)
            {
                return t < 0.5f
                  ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * t, 2))) / 2
                  : (Mathf.Sqrt(1 - Mathf.Pow(-2 * t + 2, 2)) + 1) / 2;
            }

            float EaseInBack(float t)
            {
                float c1 = 1.70158f;
                float c3 = c1 + 1;

                return c3 * t * t * t - c1 * t * t;
            }
            float EaseOutBack(float t)
            {
                float c1 = 1.70158f;
                float c3 = c1 + 1;

                return 1 + c3 * Mathf.Pow(t - 1, 3) + c1 * Mathf.Pow(t - 1, 2);
            }
            float EaseInOutBack(float t)
            {
                float c1 = 1.70158f;
                float c2 = c1 * 1.525f;

                return t < 0.5f
                  ? (Mathf.Pow(2 * t, 2) * ((c2 + 1) * 2 * t - c2)) / 2
                  : (Mathf.Pow(2 * t - 2, 2) * ((c2 + 1) * (t * 2 - 2) + c2) + 2) / 2;
            }

            float EaseInElastic(float t)
            {
                float c4 = (2 * Mathf.PI) / 3;

                return t == 0
                  ? 0
                  : t == 1
                  ? 1
                  : -Mathf.Pow(2, 10 * t - 10) * Mathf.Sin((t * 10 - 10.75f) * c4);
            }
            float EaseOutElastic(float t)
            {
                float c4 = (2 * Mathf.PI) / 3;

                return t == 0
                  ? 0
                  : t == 1
                  ? 1
                  : Mathf.Pow(2, -10 * t) * Mathf.Sin((t * 10 - 0.75f) * c4) + 1;
            }
            float EaseInOutElastic(float t)
            {
                float c5 = (2 * Mathf.PI) / 4.5f;
                return t == 0
                        ? 0 : t == 1
                        ? 1 : t < 0.5
                        ? -(Mathf.Pow(2, 20 * t - 10) * Mathf.Sin((20 * t - 11.125f) * c5)) / 2
                        : (Mathf.Pow(2, -20 * t + 10) * Mathf.Sin((20 * t - 11.125f) * c5)) / 2 + 1;
            }

            float EaseInBounce(float t)
            {
                return 1 - EaseOutBounce(1 - t);
            }
            float EaseOutBounce(float t)
            {
                float n1 = 7.5625f;
                float d1 = 2.75f;

                if (t < 1f / d1)
                {
                    return n1 * t * t;
                }
                else if (t < 2f / d1)
                {
                    return n1 * (t -= 1.5f / d1) * t + 0.75f;
                }
                else if (t < 2.5f / d1)
                {
                    return n1 * (t -= 2.25f / d1) * t + 0.9375f;
                }
                else
                {
                    return n1 * (t -= 2.625f / d1) * t + 0.984375f;
                }
            }
            float EaseInOutBounce(float t)
            {
                return t < 0.5f
                  ? (1 - EaseOutBounce(1 - 2 * t)) / 2
                  : (1 + EaseOutBounce(2 * t - 1)) / 2;
            }
            #endregion
        }
    }
}