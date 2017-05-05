package com.askey.activity;

import android.content.DialogInterface;
import android.os.AsyncTask;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.ListView;

import com.askey.Adapter.PoDeliverAdapter;
import com.askey.model.AppData;
import com.askey.model.BaseActivity;
import com.askey.model.CommonUtil;
import com.askey.wms.R;

public class podeliver extends BaseActivity implements OnClickListener {

	private ImageView ivRefresh;
	private ListView lvDisplay;
	private ImageView ivGohome;
	private EditText edtpodeliver_rcvnosearch;
	//
	private PoDeliverAdapter mAdapter;

	@Override
	protected void onCreate(Bundle savedInstanceState) {

		if (AppData.orgid.equals("")) {
			CommonUtil.WMSToast(podeliver.this, "請先選擇ORG");
			finish();
		}
		super.onCreate(savedInstanceState);
		setContentView(R.layout.podeliver);// 设置布局文件
		//
		ivGohome = (ImageView) this.findViewById(R.id.podeliver_gohome);
		ivGohome.setOnClickListener(this);
		//
		ivRefresh = (ImageView) this.findViewById(R.id.podeliver_refresh);
		lvDisplay = (ListView) findViewById(R.id.podeliver_display);
		ivRefresh.setOnClickListener(this);
		//
		edtpodeliver_rcvnosearch = (EditText) findViewById(R.id.podeliver_rcvnosearch);
		//
		mAdapter = new PoDeliverAdapter(this);
	}

	public void onClick(View v) {
		CommonUtil.trimandupper(edtpodeliver_rcvnosearch);
		//
		switch (v.getId()) {
		case R.id.podeliver_gohome: {
			finish();
			break;
		}
		case R.id.podeliver_refresh: {
			PORCVNoQuery();
			break;
		}
		}
	}

	public void PORCVNoQuery() {
		new PORCVNoQueryAsyncTask().execute();
	}

	// 参数1：传入doInBackground的参数类型
	// 参数2：传入onProgressUpdate的参数类型
	// 参数3：传入onPostExecute的参数类型，也是doInBackground的返回类型
	class PORCVNoQueryAsyncTask extends AsyncTask<Void, Void, Boolean> {
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
			lvDisplay.setAdapter(mAdapter);// 顯示資料
			CommonUtil.feedbackqueryres(podeliver.this, result);

		}

		@Override
		protected Boolean doInBackground(Void... params) {
			try {
				String strResponse = PoDeliverAdapter.Request(podeliver.this,
						edtpodeliver_rcvnosearch.getText().toString(),
						AppData.orgid);
				PoDeliverAdapter.Resolve(strResponse);
				return true;
			} catch (Exception e) {
				return false;
			}

		}
	}
}
