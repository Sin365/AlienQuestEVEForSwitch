using UnityEngine;
using System.Collections.Generic;

public class Puppet2D_GlobalControl : MonoBehaviour
{
	public float startRotationY;
	public List<Puppet2D_SplineControl> _SplineControls;
	public List<Puppet2D_IKHandle> _Ikhandles;
	public List<Puppet2D_ParentControl> _ParentControls;
	public List<Puppet2D_FFDLineDisplay> _ffdControls;
	public List<SpriteRenderer> _Controls;
	public List<SpriteRenderer> _Bones;
	public List<SpriteRenderer> _FFDControls;
	public bool ControlsVisiblity;
	public bool BonesVisiblity;
	public bool FFD_Visiblity;
	public bool CombineMeshes;
	public bool flip;
	public bool AutoRefresh;
	public bool ControlsEnabled;
	public bool lateUpdate;
	public int _flipCorrection;
}
