package com.askey.Adapter;

import android.app.Activity;
import android.content.Context;
import android.graphics.Color;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.askey.model.AppData;
import com.askey.model.CommonUtil;
import com.askey.wms.R;

public class ChooseorgAdapter extends BaseAdapter {
	private Context mContext;

	// private AppData mchooseorgAppData = new AppData();

	public ChooseorgAdapter(Context context) {
		mContext = context;
	}

	public int getCount() {
		// return mchooseorgAppData.mchooseorg.size();
		return AppData.mchooseorg.size();
	}

	public Object getItem(int position) {
		return null;
	}

	public long getItemId(int position) {
		return 0;
	}

	class ChooseorgViewHolder {
		TextView morgdesc;
		TextView morg;
	}

	public View getView(final int position, View convertView, ViewGroup parent) {
		ChooseorgViewHolder holder = null;
		//
		if (convertView == null) {
			convertView = LayoutInflater.from(mContext).inflate(
					R.layout.chooseorgitem, null);
			//
			holder = new ChooseorgViewHolder();
			holder.morgdesc = (TextView) convertView
					.findViewById(R.id.tvchooseorg_orgdesc);
			holder.morg = (TextView) convertView
					.findViewById(R.id.tvchooseorg_org);
			//
			convertView.setTag(holder);

		} else {
			holder = (ChooseorgViewHolder) convertView.getTag();
		}
		//if (position == 0) {// 标题行居中
			/*
			convertView.setBackgroundColor(convertView.getResources().getColor(
					R.color.galy));
			*/
			//holder.morgdesc.setGravity(Gravity.CENTER);
			//holder.morg.setGravity(Gravity.CENTER);
		//}
		//隔行換顏色
		int[] colors = { Color.WHITE, Color.rgb(219, 238, 244) };// RGB颜色
		//convertView.setBackgroundColor(colors[position % 2]);// item之间颜色不同 
		holder.morgdesc.setBackgroundColor(colors[position % 2]);
		holder.morg.setBackgroundColor(colors[position % 2]);
		//
		holder.morgdesc.setWidth((int) (AppData.screenwidth * 0.5));
		holder.morg.setWidth((int) (AppData.screenwidth * 0.5));
		//
		holder.morgdesc.setText(AppData.mchooseorg.get(position).Getorgdesc());
		holder.morg.setText(AppData.mchooseorg.get(position).Getorg());

		// 選擇org,關閉窗體
		convertView.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				//
				if (position >= 1) {
					AppData.orgid = AppData.mchooseorg.get(position).Getorg();
					AppData.orgcode = AppData.mchooseorg.get(position)
							.Getorgdesc().trim();
					// 提示ORG
					CommonUtil
							.WMSToast(mContext, AppData.orgcode+ " | "+AppData.orgid);
					// 如何動態關閉選org的activity???
					((Activity) mContext).finish();
				}
			}
		});
		//
		return convertView;

	}

}
