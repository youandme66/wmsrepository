package com.askey.activity;

import org.json.JSONArray;
import org.json.JSONObject;

import android.content.Context;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;

import com.askey.Adapter.LoginAdapter;
import com.askey.model.AppData;
import com.askey.model.BaseActivity;
import com.askey.model.CommonUtil;
import com.askey.model.K_table;
import com.askey.wms.R;

public class login extends BaseActivity implements OnClickListener {

	private Button btnLogin;
	private EditText edUserName;
	private EditText edUserPwd;

	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.login);
		btnLogin = (Button) this.findViewById(R.id.user_login_btn);
		edUserName = (EditText) this.findViewById(R.id.login_user_name);
		edUserPwd = (EditText) this.findViewById(R.id.login_user_pwd);
		//
		btnLogin.setOnClickListener(this);
		//
		edUserName.requestFocus();
		//
		String ip = getIP();
		//CommonUtil.WMSShowmsg(login.this, ip);
		if(!ip.equals(getString(R.string.myip)) && !ip.equals(getString(R.string.myip1)))
		{//如果不是自己測試則清除賬號和密碼
			edUserName.setText("");
			edUserPwd.setText("");
		}
	}
    
	public void onClick(View v) {
		switch (v.getId()) {
		case R.id.user_login_btn:
			//CommonUtil.Inputbox(this,"請輸入用戶名",edUserName);
			//AppData.BoxID_1="http://10.7.46.169/WMSService_Android/WebService_Android.asmx/";
			//String s=CommonUtil.getValue(login.this,"IP","null");
			//CommonUtil.WMSShowmsg(login.this, s);
			//CommonUtil.putValue(login.this,"IP","10.10.10.1");
			CheckUser();
			// CommonUtil.WMSShowmsg(this, "login ok");
			// txtUserName.setText(AppData.dialogresult);
			break;
		}
	}

	public void CheckUser() {
		// new loginAsyncTask().execute();
		// new logininAsyncTask(txtUserName.getText().toString(),
		// txtUserPwd.getText().toString()).execute();
		new logininAsyncTask().execute(edUserName.getText().toString(),
				edUserPwd.getText().toString());
	}

	private final class logininAsyncTask extends
			AsyncTask<String, Void, Boolean> {
		@Override
		protected Boolean doInBackground(String... params) {
			try {
				AppData.user_id = "";
				K_table.excuteres="";
				// String strResponse =loginAdapter.Request(login.this,username,pwd);
				String strResponse = LoginAdapter.Request(login.this,
						params[0], params[1]);
                //
				LoginAdapter.Resolve(strResponse);
				//
				if (AppData.user_id.equals(""))
				{
					//JSONObject json = new JSONObject(strResponse);
					//K_table.excuteres=json.getString("result");
					return false;
				}
				else
					return true;
			} catch (Exception e) {
				K_table.excuteres=e.getMessage().toString();
				return false;
			}
		}

		protected void onPostExecute(Boolean result) {
			//GYM为了测试
			result = true;
			try {
				if (result) {// login success
					AppData.user_name = edUserName.getText().toString();
					openActivity(main.class);
					defaultFinish();
				} 
				else// login fail
				{
					if(K_table.excuteres.equals(""))//執行正常但是用戶名或密碼不對
					  CommonUtil.WMSShowmsg(login.this,getString(R.string.user_login_error));
					else//執行有異常
				      CommonUtil.WMSShowmsg(login.this,K_table.excuteres);
				}
			} catch (Exception e) {
				
			}
		}
	}
	/*
	 * class loginAsyncTask extends AsyncTask<String, Void, Boolean> {
	 * 
	 * @Override protected void onPreExecute() { // TODO Auto-generated method
	 * stub super.onPreExecute(); showProgressDialog("", "加載中...", new
	 * DialogInterface.OnCancelListener() { public void onCancel(DialogInterface
	 * dialog) { } }); }
	 * 
	 * @Override protected Boolean doInBackground(String... params) { try {
	 * AppData.user_id = ""; String strResponse =
	 * loginAdapter.Request(login.this, txtUserName.getText().toString(),
	 * txtUserPwd.getText() .toString()); loginAdapter.Resolve(strResponse);
	 * return true; } catch (Exception e) { return false; } }
	 * 
	 * @Override protected void onPostExecute(Boolean result) {
	 * super.onPostExecute(result); try { mAlertDialog.dismiss(); if
	 * (AppData.user_id.equals("")) {
	 * //showShortToast(R.string.user_login_error);
	 * CommonUtil.WMSShowmsg(login.this, getString(R.string.user_login_error));
	 * } else { AppData.user_name=txtUserName.getText().toString();
	 * openActivity(main.class); defaultFinish(); } } catch (Exception e) { } }
	 * }
	 */
}
