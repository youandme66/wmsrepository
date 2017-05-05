package com.askey.activity;

import android.content.DialogInterface;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.Spinner;

import com.askey.Adapter.ChooseorgAdapter;
import com.askey.Adapter.ChooseorgHelper;
import com.askey.model.AppData;
import com.askey.model.BaseActivity;
import com.askey.wms.R;

public class chooseOrg extends BaseActivity implements OnClickListener {
	private ImageView ivGohome;
	private ListView lvchooseorg;
	private ImageView ivrefresh;
	private ChooseorgAdapter mchooseorgAdapter;//
	private ChooseorgHelper chooseorgHelper = new ChooseorgHelper();
	private Spinner mspinner;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.chooseorg);// 设置布局文件
		//
		ivGohome = (ImageView) this.findViewById(R.id.chooseorg_gohome);
		lvchooseorg = (ListView) this.findViewById(R.id.lvchooseorg);
		ivrefresh = (ImageView) this.findViewById(R.id.chooseorg_refresh);
		//
		//mspinner=(Spinner) findViewById(R.id.spinner1);
		//mspinner.setAdapter(null);
		//
		ivGohome.setOnClickListener(this);
		ivrefresh.setOnClickListener(this);
		//
		mchooseorgAdapter = new ChooseorgAdapter(this);
		//
		GetORG();
	}

	public void onClick(View v) {
		switch (v.getId()) {
		case R.id.chooseorg_gohome: {
			finish();
			break;
		}
		case R.id.chooseorg_refresh: {
			GetORG();
			break;
		}
		}
	}

	public void GetORG() {
		new GetORGAsyncTask().execute();
	}

	// 参数1：传入doInBackground的参数类型
	// 参数2：传入onProgressUpdate的参数类型
	// 参数3：传入onPostExecute的参数类型，也是doInBackground的返回类型
	class GetORGAsyncTask extends AsyncTask<Void, Void, Boolean> {
		@Override
		protected void onPreExecute() {
			super.onPreExecute();
			showProgressDialog("", "加载中...",
					new DialogInterface.OnCancelListener() {
						public void onCancel(DialogInterface dialog) {
						}
					});
		}

		@Override
		protected void onPostExecute(Boolean result) {
			super.onPostExecute(result);
			mAlertDialog.dismiss();
			//
			lvchooseorg.setAdapter(mchooseorgAdapter);
			//mspinner.setAdapter(mchooseorgAdapter);
			//mspinner.setSelection(1,true);//設置選中哪一條,1表示第二條
			
//			if (result) {
//				lvchooseorg.setAdapter(mchooseorgAdapter);// 顯示資料
//			}
			// CommonUtil.feedbackqueryres(chooseorg.this, result);
		}

		@Override
		protected Boolean doInBackground(Void... params) {
			try {
				String strResponse = chooseorgHelper.Request(chooseOrg.this,
						AppData.user_id);
				chooseorgHelper.Resolve(strResponse);
				return true;
			} catch (Exception e) {
				return false;
			}
		}
	}
}
