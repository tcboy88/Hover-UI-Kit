using Hover.Board.State;
using Hover.Common.Custom;
using Hover.Common.Display;
using Hover.Common.State;
using UnityEngine;

namespace Hover.Board.Display.Standard {

	/*================================================================================================*/
	public abstract class UiItemBaseToggleRenderer : UiItemSelectRenderer {

		protected GameObject vOuter;
		protected GameObject vInner;
		protected Mesh vOuterMesh;
		protected Mesh vInnerMesh;

		private int vPrevTextSize;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		protected abstract Materials.IconOffset GetOuterIconOffset();

		/*--------------------------------------------------------------------------------------------*/
		protected abstract Materials.IconOffset GetInnerIconOffset();

		/*--------------------------------------------------------------------------------------------*/
		protected abstract bool IsToggled();


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public override void Build(IHoverboardPanelState pPanelState,
										IHoverboardLayoutState pLayoutState, IBaseItemState pItemState,
										IItemVisualSettings pSettings) {
			base.Build(pPanelState, pLayoutState, pItemState, pSettings);

			vLabel.AlignLeft = true;
			vLabel.transform.localPosition = new Vector3(-vItemState.Item.Width/2, 0, 0);

			////

			vOuter = GameObject.CreatePrimitive(PrimitiveType.Quad);
			vOuter.name = "ToggleOuter";
			vOuter.transform.SetParent(gameObject.transform, false);
			vOuter.transform.localRotation = vLabel.CanvasLocalRotation;

			vOuter.GetComponent<MeshRenderer>().sharedMaterial = Materials.StandardIcons;

			vOuterMesh = vOuter.GetComponent<MeshFilter>().mesh;
			Materials.SetMeshColor(vOuterMesh, Color.clear);
			Materials.SetMeshIconCoords(vOuterMesh, GetOuterIconOffset());

			////

			vInner = GameObject.CreatePrimitive(PrimitiveType.Quad);
			vInner.name = "ToggleInner";
			vInner.transform.SetParent(gameObject.transform, false);
			vInner.transform.localRotation = vLabel.CanvasLocalRotation;

			vInner.GetComponent<MeshRenderer>().sharedMaterial = Materials.StandardIcons;

			vInnerMesh = vInner.GetComponent<MeshFilter>().mesh;
			Materials.SetMeshColor(vInnerMesh, Color.clear);
			Materials.SetMeshIconCoords(vInnerMesh, GetInnerIconOffset());
		}

		/*--------------------------------------------------------------------------------------------*/
		public override void Update() {
			base.Update();

			Color color = vSettings.ToggleIconColor;
			color.a *= (vItemState.MaxHighlightProgress*0.25f + 0.75f)*vMainAlpha;

			Materials.SetMeshColor(vOuterMesh, color);
			Materials.SetMeshColor(vInnerMesh, color);
			vInner.SetActive(IsToggled());

			if ( vSettings.TextSize != vPrevTextSize ) {
				vPrevTextSize = vSettings.TextSize;

				float inset = vSettings.TextSize;
				Vector3 pos = vLabel.transform.localPosition+
					new Vector3(inset*0.666f*LabelCanvasScale, 0, 0);
				Vector3 scale = Vector3.one*(vSettings.TextSize*0.75f*LabelCanvasScale);

				vLabel.SetInset(true, inset);

				vOuter.transform.localPosition = pos;
				vOuter.transform.localScale = scale;

				vInner.transform.localPosition = pos;
				vInner.transform.localScale = scale;
			}
		}

	}

}