using UnityEngine;
using System.Collections;

static public class Tk2dExtensions
{
	static public void PlayIfNotPlaying(this tk2dSpriteAnimator animator, string name)
	{
		if(!animator.IsPlaying(name))
		{
			animator.Play(name);	
		}
	}
	
	public static void SetText(this tk2dTextMesh mesh, string newText)
    {
        mesh.text = newText;
        mesh.Commit();
    }
}
