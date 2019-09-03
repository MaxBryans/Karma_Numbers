using UnityEngine;

public enum Tween
{
            LinearUp,
            LinearDown,
            ParametricUp,
            ParametricDown,
            QuadUp,
            QuadDown,
            QuinUp,
            QuinDown,
            SineUp,
            SineDown,
            SinePop,
            BounceUp,
            BounceDown,
            BouncePop
}

public class TweenFactory : MonoBehaviour
{
    #region Public Methods
    public float DoTween(Tween tween, float t)
    {
        // LinearUp, LinearDown, ParametricUp, ParametricDown, QuadUp, QuadDown, QuinUp, QuinDown, SineUp, SineDown,BounceUp, BounceDown, SinePop
        switch (tween)
        {
            case Tween.LinearUp: return LinearUp(t);
            case Tween.LinearDown: return LinearDown(t);
            case Tween.ParametricUp: return ParametricUp(t);
            case Tween.ParametricDown: return ParametricDown(t);
            case Tween.QuadUp: return QuadUp(t);
            case Tween.QuadDown: return QuadDown(t);
            case Tween.QuinUp: return QuinUp(t);
            case Tween.QuinDown: return QuinDown(t);
            case Tween.SineUp: return SineUp(t);
            case Tween.SineDown: return SineDown(t);
            case Tween.SinePop: return SinePop(t);
            case Tween.BounceUp: return BounceUp(t);
            case Tween.BounceDown: return BounceDown(t);
            case Tween.BouncePop: return BouncePop(t);
            default: return t;
        }
    }

    public Vector3 DoTweenV3 (Vector3 start, Vector3 end, Tween tween, float t)
    {
        return Vector3.Lerp(start, end, DoTween(tween, t));
    }

    public Vector2 DoTweenV2 (Vector2 start, Vector2 end, Tween tween, float t)
    {
        return Vector2.Lerp(start, end, DoTween(tween, t));
    }
    #endregion

    #region Private Methods

    // TWEENING FUNCTIONS
    private float LinearUp(float t)
    {
        return Sanitise(t);
    }
    private float LinearDown(float t)
    {
        t = Sanitise(t);
        return 1f - t;
    }

    private float ParametricUp(float t)
    {
        t = Sanitise(t);
        float sqr = t * t;
        return sqr / (2.0f * (sqr - t) + 1.0f);
    }
    private float ParametricDown(float t)
    {
        t = Sanitise(t);
        return ParametricUp(1f - t);
    }

    private float QuadUp(float t)
    {
        t = Sanitise(t);
        return t * t;
    }
    private float QuadDown(float t)
    {
        t = Sanitise(t);
        return QuadUp(1f - t); ;
    }

    private float QuinUp(float t)
    {
        t = Sanitise(t);
        return t * t * t * t * t;
    }
    private float QuinDown(float t)
    {
        t = Sanitise(t);
        return QuinUp(1f - t); ;
    }

    private float SineUp(float t)
    {
        t = Sanitise(t);
        return Mathf.Sin(Mathf.PI / 2 * t);
    }
    private float SineDown(float t)
    {
        t = Sanitise(t);
        return SineUp(1f - t);
    }

    private float SinePop(float t)
    {
        t = Sanitise(t);
        return Mathf.Sin(Mathf.PI * t);
    }

    //// Thanks to https://github.com/d3/d3-ease/blob/master/src/bounce.js#L12 (24 Nov 2018)
    float BounceUp(float t)
    {
        t = Sanitise(t);
        float b1 = 4f / 11f,
            b2 = 6f / 11f,
            b3 = 8f / 11f,
            b4 = 3f / 4f,
            b5 = 9f / 11f,
            b6 = 10f / 11f,
            b7 = 15f / 16f,
            b8 = 21f / 22f,
            b9 = 63f / 64f,
            b0 = 1f / b1 / b1;
        if (t < b1) return b0 * t * t;
        else if (t < b3)
        {
            t = t - b2;
            return b0 * t * t + b4;
        }
        else if (t < b6)
        {
            t = t - b5;
            return b0 * t * t + b7;
        }
        else
        {
            t = t - b8;
            return b0 * t * t + b9;
        }
    }
    float BounceDown(float t)
    {
        t = Sanitise(t);
        return BounceUp(1f - t);
    }
    float BouncePop(float t)
    {
        t = Sanitise(t);
        if (t <= 0.5f)
        {
            return BounceUp(t * 2f);
        }
        else
        {
            return BounceDown((t - 0.5f) * 2f);
        }
    }

    private float Sanitise(float t)
    {
        if (t < 0.0f) return 0.0f;
        else if (t > 1.0f) return 1.0f;
        else return t;
    }
    #endregion
}
