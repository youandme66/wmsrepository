package com.askey.Adapter;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.askey.activity.changeDCFrame_Detail;
import com.askey.model.AppData;
import com.askey.wms.R;

public class ChangedcframeAdapter extends BaseAdapter {
	private Context mContext;
	private AppData mAppData = new AppData();

	public ChangedcframeAdapter(Context context) {
		mContext = context;
	}

	public int getCount() {
		return mAppData.mchangedcframedetail.size();
	}

	public Object getItem(int position) {
		return null;
	}

	public long getItemId(int position) {
		return 0;
	}

	public View getView(final int position, View convertView, ViewGroup parent) {
		ViewHolder holder = null;
		//
		if (convertView == null) {
			convertView = LayoutInflater.from(mContext).inflate(
					R.layout.changedcframeitem, null);
			//
			holder = new ViewHolder();
			holder.munique_id = (TextView) convertView
					.findViewById(R.id.changedcframeitem_unique_id);
			holder.mitem_name = (TextView) convertView
					.findViewById(R.id.changedcframeitem_item_name);
			holder.msubinventory_name = (TextView) convertView
					.findViewById(R.id.changedcframeitem_subinv);
			holder.monhand_qty = (TextView) convertView
					.findViewById(R.id.changedcframeitem_onhandqty);
			holder.msimulated_qty = (TextView) convertView
					.findViewById(R.id.changedcframeitem_simulatedqty);
			holder.mdatecode = (TextView) convertView
					.findViewById(R.id.changedcframeitem_datecode);
			holder.mframe = (TextView) convertView
					.findViewById(R.id.changedcframeitem_frame);
			//
			convertView.setTag(holder);
		} else {
			holder = (ViewHolder) convertView.getTag();
		}
		//
		holder.munique_id.setWidth((int)(AppData.screenwidth*AppData.colwidth.cuniqueid));
		holder.mitem_name.setWidth((int)(AppData.screenwidth*AppData.colwidth.citemname));
		holder.msubinventory_name.setWidth((int)(AppData.screenwidth*AppData.colwidth.csubinv));
		holder.monhand_qty.setWidth((int)(AppData.screenwidth*AppData.colwidth.cqty));
		holder.msimulated_qty.setWidth((int)(AppData.screenwidth*AppData.colwidth.cqty));
		holder.mdatecode.setWidth((int)(AppData.screenwidth*AppData.colwidth.cdc));
		holder.mframe.setWidth((int)(AppData.screenwidth*AppData.colwidth.cframe));
		//
		holder.munique_id.setText(mAppData.mchangedcframedetail.get(position)
				.Getunique_id());
		holder.mitem_name.setText(mAppData.mchangedcframedetail.get(position)
				.Getitem_name());
		holder.msubinventory_name.setText(mAppData.mchangedcframedetail.get(
				position).Getsubinventory_name());
		holder.monhand_qty.setText(mAppData.mchangedcframedetail.get(position)
				.Getonhand_qty());
		holder.msimulated_qty.setText(mAppData.mchangedcframedetail.get(
				position).Getsimulated_qty());
		holder.mdatecode.setText(mAppData.mchangedcframedetail.get(position)
				.Getdatecode());
		holder.mframe.setText(mAppData.mchangedcframedetail.get(position)
				.Getframe());
		//
		convertView.setOnClickListener(new OnClickListener() {
			public void onClick(View v) {
				Intent intent = new Intent(mContext, changeDCFrame_Detail.class);// 创建Intent对象
				//
				if (position >= 1) {
					intent.putExtra("unique_id", mAppData.mchangedcframedetail
							.get(position).Getunique_id());
					intent.putExtra("item_name", mAppData.mchangedcframedetail
							.get(position).Getitem_name());
					intent.putExtra("subinventory_name",
							mAppData.mchangedcframedetail.get(position)
									.Getsubinventory_name());
					intent.putExtra("onhand_qty", mAppData.mchangedcframedetail
							.get(position).Getonhand_qty());
					intent.putExtra("simulated_qty",
							mAppData.mchangedcframedetail.get(position)
									.Getsimulated_qty());
					intent.putExtra("datecode", mAppData.mchangedcframedetail
							.get(position).Getdatecode());
					intent.putExtra("frame",
							mAppData.mchangedcframedetail.get(position)
									.Getframe());
					//
					mContext.startActivity(intent);// 执行Intent操作
				}
			}
		});
		//
		//int[] colors = { Color.WHITE, Color.rgb(219, 238, 244) };// RGB颜色
		//convertView.setBackgroundColor(colors[position % 2]);// 每隔item之间颜色不同
		//
		return convertView;
	}

	class ViewHolder {
		TextView munique_id;
		TextView mitem_name;
		TextView msubinventory_name;
		TextView monhand_qty;
		TextView msimulated_qty;
		TextView mdatecode;
		TextView mframe;
	}
}
