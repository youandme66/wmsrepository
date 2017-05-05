package com.askey.activity;

import com.askey.model.BaseActivity;

import android.os.Bundle;
import android.os.Handler;
import android.os.Message;



public class splashActivity extends BaseActivity{
	public Handler mHandler = new Handler() {
		@Override
		public void handleMessage(Message msg) {
			// TODO Auto-generated method stub
			super.handleMessage(msg);
			goLogin();
		}
	};

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
	}
	
	@Override
	protected void onResume() {
		// TODO Auto-generated method stub
		super.onResume();
		mHandler.removeMessages(0);
		mHandler.sendEmptyMessageDelayed(0, 2000);
	}
	
	public void goLogin()
	{
		openActivity(login.class);
		defaultFinish();
	}
}


