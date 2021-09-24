using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyUSC.Classes
{
	public class MsgButton : ImageButton
	{
		public enum ButtonOps { Delete, Reply }

		protected long m_lMsgID;
		protected ButtonOps m_btnOp;

		public MsgButton() : base()
		{
			m_lMsgID = 0;
			m_btnOp = ButtonOps.Delete;
		}

		protected override void OnClick( ImageClickEventArgs e )
		{
			base.OnClick( e );


		}

		public long MessageID
		{
			get
			{
				return this.m_lMsgID;
			}
			set
			{
				this.m_lMsgID = value;
			}
		}

		public ButtonOps ButtonOp
		{
			get
			{
				return this.m_btnOp;
			}
			set
			{
				this.m_btnOp = value;
			}
		}

	}
}