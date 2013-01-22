using System;
using Data;
namespace Controllers
{
	public class PivotController
	{
		RigidBodyController body;
		Pivot pivot;
		public PivotController (RigidBodyController body, Pivot pivot)
		{
			this.body = body;
			this.pivot = pivot;
			
			body.addPivot(pivot);
		}
	}
}

