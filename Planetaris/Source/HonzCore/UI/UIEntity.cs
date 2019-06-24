using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HonzCore.UI
{
	public class UIEntity
	{
		private bool isCreated;
		private bool enabled;
		private bool isDestroyed;
		private bool isRoot;

		private bool isInScene;
		private bool isInActiveScene;

		public bool IsCreated { get => isCreated; set => isCreated = value; }
		public bool Enabled { get => enabled; set => enabled = value; }
		public bool IsDestroyed { get => isDestroyed; set => isDestroyed = value; }
		public bool IsRoot { get => isRoot; set => isRoot = value; }
		public bool IsInScene { get => isInScene; set => isInScene = value; }
		public bool IsInActiveScene { get => true; set => isInActiveScene = value; } //TODO

		private List<UIEntity> children = new List<UIEntity>();
		private UIEntity parent;

		public UIEntity()
		{

		}

		public void Update()
		{
			foreach (UIEntity child in children)
			{
				child.Update();
			}
		}
		public void Draw()
		{
			foreach (UIEntity child in children)
			{
				child.Draw();
			}
		}

		public void SetParent(UIEntity parent)
		{
			//Remove self from old parent.
			if (this.parent != null)
			{
				this.parent.children.Remove(this);
			}

			//Add self to new parent.
			this.parent = parent;
			if (this.parent != null)
			{
				this.parent.children.Add(this);
			}

			//Create self.
			if (!isCreated && parent != null && parent.IsCreated && isInActiveScene)
			{
				CallCreate();
			}
		}

		//Creats self and children.
		public void CallCreate()
		{
			if (isCreated)
			{
				return;
			}

			foreach (UIEntity child in children)
			{
				child.CallCreate();
			}

			isCreated = true;
		}

		public void Destroy()
		{
			isDestroyed = true;

			foreach (UIEntity child in children)
			{
				child.Destroy();
			}
		}

		~UIEntity()
		{
			if (!isDestroyed)
				Destroy();
		}
	}
}
