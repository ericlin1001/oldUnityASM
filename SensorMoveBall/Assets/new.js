#pragma strict

//
//С�����ͼ
var round : Texture2D;
 
//С������Ļ����ʾ��X Y����
var x = 0;
var y = 0;
 
//С����Ļ��ʾ����� X Y ��Χ
var cross_x = 0;
var cross_y = 0;
 
function Start(){
//��ʼ����ֵ
cross_x = Screen.width - round.width;
cross_y = Screen.height - round.height;
}
 
function OnGUI () {
 
//������ʾ x y z ������Ӧ����������
GUI.Label(Rect(0,0,480,100),"position is " + Input.acceleration);
 
//����С��
GUI.DrawTexture(Rect(x,y,256,256),round);
}
 
function Update(){
 
//�������������޸�С���λ���������30����˼����С���ƶ��Ŀ�һЩ
x += Input.acceleration.x * 30;
y += -Input.acceleration.y * 30;
 
//����С�򳬳���Ļ
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