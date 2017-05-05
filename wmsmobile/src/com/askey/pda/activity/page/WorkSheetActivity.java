package com.askey.pda.activity.page;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.GridView;

import com.askey.pda.activity.WebViewActivity;
import com.askey.pda.adapter.PictureAdapter;
import com.askey.pda.util.UrlUtils;
import com.askey.wms.R;

public class WorkSheetActivity extends Activity {
    // 定义字符串数组，存储系统功能
    private String[] titles;
    // 定义int数组，存储功能对应的图标
    private int[] images = new int[] { R.drawable.car,
            R.drawable.box, R.drawable.checked,
            R.drawable.po_return};
    private GridView gvInfo;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_work_sheet);

        titles = this.getResources().getStringArray(R.array.WorkSheetTitle);
        gvInfo = (GridView) findViewById(R.id.gv_Info);// 获取布局文件中的gvInfo组件
        PictureAdapter adapter = new PictureAdapter(titles, images, this);// 创建pictureAdapter对象
        gvInfo.setAdapter(adapter);// 为GridView设置数据源

        gvInfo.setOnItemClickListener(new AdapterView.OnItemClickListener() {// 为GridView设置项单击事件
            @Override
            public void onItemClick(AdapterView<?> arg0, View arg1, int arg2,
                                    long arg3) {
                Intent intent = null;// 创建Intent对象
                String url = "";
                switch (arg2) {
                    case 0:
                        url = UrlUtils.getUrl("analogAcquisitionPDA.aspx");
                        break;
                    case 1:
                        url = UrlUtils.getUrl("worksheetIssuePDA.aspx");
                        break;
                    case 2:
                        url = UrlUtils.getUrl("WorkSheetPickupPDA.aspx");
                        break;
                    case 3:
                        url = UrlUtils.getUrl("WorkSheetInPDA.aspx");
                        break;
                    default:
                        break;
                }
                if (!url.equals("")) {
                    intent = new Intent(WorkSheetActivity.this, WebViewActivity.class);
                    intent.putExtra("url", url);
                    startActivity(intent);
                }
            }
        });


    }
}
