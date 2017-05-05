package com.askey.pda.activity;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.View;
import android.widget.AdapterView;
import android.widget.GridView;

import com.askey.pda.activity.page.PageWorkActivity;
import com.askey.pda.activity.page.PoWorkActivity;
import com.askey.pda.activity.page.SettingWorkActivity;
import com.askey.pda.activity.page.StorageManageActivity;
import com.askey.pda.activity.page.WorkSheetActivity;
import com.askey.pda.adapter.PictureAdapter;
import com.askey.wms.R;

public class MainActivity extends Activity {

    // 定义字符串数组，存储系统功能
    private String[] titles;
    // 定义int数组，存储功能对应的图标
    private int[] images = new int[] { R.drawable.chooseorg,
            R.drawable.changedcframe, R.drawable.podeliver,
            R.drawable.printmtllabel, R.drawable.exchange,
            R.drawable.takestock, R.drawable.exit};
    private GridView gvInfo;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        titles = this.getResources().getStringArray(R.array.indexTitle);
        gvInfo = (GridView) findViewById(R.id.gv_Info);// 获取布局文件中的gvInfo组件
        PictureAdapter adapter = new PictureAdapter(titles, images, this);// 创建pictureAdapter对象
        gvInfo.setAdapter(adapter);// 为GridView设置数据源

        gvInfo.setOnItemClickListener(new AdapterView.OnItemClickListener() {// 为GridView设置项单击事件
            @Override
            public void onItemClick(AdapterView<?> arg0, View arg1, int arg2,
                                    long arg3) {
                Intent intent = null;// 创建Intent对象
                switch (arg2) {
                    case 0:
                        intent = new Intent(MainActivity.this,SettingWorkActivity.class);// 窗口初始化Intent
                        break;
                    case 1:
                        intent = new Intent(MainActivity.this,PoWorkActivity.class);
                        break;
                    case 2:
                        intent = new Intent(MainActivity.this,WorkSheetActivity.class);
                        break;
                    case 3:
                        intent = new Intent(MainActivity.this,PageWorkActivity.class);
                        break;
                    case 4:
                        intent = new Intent(MainActivity.this,StorageManageActivity.class);
                        break;
                    case 5:
                        intent = new Intent(MainActivity.this,PrintActivity.class);
                        break;
                    case 6:
                        System.exit(0);// 退出系統
                        break;
                    default:
                        break;
                }
                if (intent != null)
                    startActivity(intent);
            }
        });
    }

    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        if ((keyCode == KeyEvent.KEYCODE_BACK)) {
            System.out.println("按下了back键   onKeyDown()");
            String url = "http://121.41.45.122/PDA/LoginPDA.aspx";
            Intent intent;
            intent = new Intent(MainActivity.this, WebViewActivity.class);
            intent.putExtra("url", url);
            startActivity(intent);
            finish();

        }
            return super.onKeyDown(keyCode, event);
    }
}
