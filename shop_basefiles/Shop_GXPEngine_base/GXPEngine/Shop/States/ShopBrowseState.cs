using GXPEngine;

namespace States
{
	using Model;
	using View;
	using Controller;

	public class ShopBrowseState : GameObject
	{
		private ShopModel shopModel;
		private readonly ShopController shopController;
		private readonly ShopView shopView;
		private readonly ShopMessageView shopMessageView;

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  ShopBrowseState()
		//------------------------------------------------------------------------------------------------------------------------
		public ShopBrowseState()
		{
			//create shop model
			shopModel = new ShopModel();

			//create shop view
			shopView = new ShopView(shopModel);
			AddChild(shopView);
			Helper.AlignToCenter(shopView, true, true);

			//create shop controller
			shopController = new ShopController(shopModel, shopView);

			//create message view
			shopMessageView = new ShopMessageView();
			AddChild(shopMessageView);
			Helper.AlignToCenter(shopMessageView, true, false);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//                                                  Step()
		//------------------------------------------------------------------------------------------------------------------------
		//update the views
		public void Step()
		{
			shopController.Step();
			shopView.Step();
			shopMessageView.Step();
		}

		protected override void OnDestroy()
		{
			shopModel.Destroy();
			shopModel = null;
			
			base.OnDestroy();
		}
	}
}