#pragma strict

//
//小球的贴图
var round : Texture2D;
 
//小球在屏幕中显示的X Y坐标
var x = 0;
var y = 0;
 
//小球屏幕显示的最大 X Y 范围
var cross_x = 0;
var cross_y = 0;
 
function Start(){
//初始化赋值
cross_x = Screen.width - round.width;
cross_y = Screen.height - round.height;
}
 
function OnGUI () {
 
//整体显示 x y z 重力感应的重力分量
GUI.Label(Rect(0,0,480,100),"position is " + Input.acceleration);
 
//绘制小球
GUI.DrawTexture(Rect(x,y,256,256),round);
}
 
function Update(){
 
//根据重力分量修改小球的位置这里乘以30的意思是让小球移动的快一些
x += Input.acceleration.x * 30;
y += -Input.acceleration.y * 30;
 
//避免小球超出屏幕
if(x < 0){
x = 0;
}else if(x > cross_x){
x = cross_x;
}
 
if(y < 0){
y = 0;
}else if(y > cross_y){
y = cross_y;
}
}