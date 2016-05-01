﻿using UnityEngine;

namespace Hover.Board.Renderers.Fills {

	/*================================================================================================*/
	[ExecuteInEditMode]
	public abstract class HoverRendererFill : MonoBehaviour {
	
		public bool ControlledByRenderer { get; set; }
		
		[HideInInspector]
		[SerializeField]
		private bool vIsBuilt;
		
		
		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public abstract int MaterialRenderQueue { get; }
		

		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public virtual void Awake() {
			if ( !vIsBuilt ) {
				BuildElements();
				vIsBuilt = true;
			}
		}
		
		/*--------------------------------------------------------------------------------------------*/
		public virtual void Update() {
			if ( !ControlledByRenderer ) {
				UpdateAfterRenderer();
			}
		}
		
		/*--------------------------------------------------------------------------------------------*/
		public abstract void UpdateAfterRenderer();
		

		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		protected abstract void BuildElements();
				
	}

}