# NonActiveX-HTTPAgent
NonActiveX (for deal with hardware device or os system program like secure program) on Web Environment

NonAcitveX를 위한 Ajax 비동기 Agent

NonActiveX 개요

 NonActiveX는 기존의 웹브라우저(Internet Explorer 환경)에서 사용하던 ActiveX(OCX)를 모두 제거 하고
기존 웹 화면과 같은 기능을 수행하는 EXE(실행파일)를 브라우저에서 실행, 브라우저와 스캔 EXE간의 통신, I/O를 가능하게 하여 웹에서 호출 한 EXE 수행 결과 값을 브라우저로 리턴 받을 수 있습니다.
중간 통신의 EXE와 브라우저간의 직접적인 통신이 어려워 로컬 웹 서버를 이용해 
통신을 가능하게끔 합니다.
(행안부는 기존 ActiveX형태가 있던 것이 아니라 [As-Is : CS형식 – Delphi 개발]에서 
차세대 시스템이 웹기반으로 도입, 전환함에 따라 NonAcitveX 방식 도입)
  브라우저 단에서는 EXE를 실행시키기 위해 custom URI를 사용하고 로컬 웹 서버와 Ajax 통신을 통하여 EXE와 통신 및 I/O작업을 할 수 있습니다.
 제약사항 : OS – Windows 환경 Windows 7 SP2 (.NET 4.5)이상 사용가능
 브라우저 – 웹 표준을 준수하는 모든 브라우저 사용가능 (Internet Explorer 10 이상)
1.	구조
 
  WEB과 EXE를 통신시키기 위해서는 사용자 PC에 Local Web Server(Inzi_MoisAgent.exe)가 필요하며,
이 Local Web Server(Inzi_MoisAgent.exe)는 설치가 정상적으로 되었다면 윈도우 시작프로그램에 등록되어 
사용자가 컴퓨터를 실행할 때 자동 부팅됩니다.
(만약, 서버가 실행 중에 오류가 생겨 종료되더라도, API를 통해 실행시킬 수 있습니다.)






2.	방법
  브라우저단에서 inzi_iFormScan NonActiveX를 사용하기 위한 단계는 다음과 같습니다.
 

1. EXE 실행 및 Param값 전달 (Ajax – HTTP Request)
    EXE실행 및 Interface로 정의된 Param값을 전달 

2. EXE 작업 후 결과 값 콜백함수로 받아오기 (Ajax – HTTP Response)
    콜백 인자 : 결과값(result) – json string

 
Mois_InziJS.js (공통 js파일 API 설명)

<script type="text/javascript" src="jquery-1.11.2.min.js" ></script>
<script type="text/javascript" src="inziJS-1.0.js"></script>
	넥사크로 개발툴 이용시 따로 위 js모듈 Object import하는 .json파일 제공

1. 개요
 • InziEXE_Init() : Inzi_MoisAgent 실행
 • InziEXE_GetReturn (interface, callbackFunc) : EXE 실행 (Interface 형식에 맞게) 및 callback함수 리턴
 • SetEXETimeout (time) : InziEXE_GetReturn() 요청 후 리턴값을 기다리는 최대 시간을 설정합니다.
 • GetCustomURI() : Inzi_MoisAgent를 실행시키기 위한 Custom URI값을 가져옵니다.

2.	상세

• InziEXE_Init()
로컬웹서버(Inzi_MoisAgent.exe)를 실행시킵니다.
파라미터 – 없음
리턴값 – 없음
사용방법 - InziEXE_GetReturn()을 통해 Inzi 솔루션을 이용할 때 서버 응닶값이 없을경우 혹은 
{
	errorStatus : 0,
	errorMsg : “Server None”
}
과 같은 값이 떨어질 경우에 해당 API를 호출해서 Inzi_MoisAgent.exe를 실행시켜줍니다

예시)
  

• InziEXE_GetReturn(interface, callbackFunc)
EXE를 Interface객체로 요청한 대로 작동합니다.(정해진 Interface형식에 맞게 전달해야 됨.)
파라미터 – interface : 정해진 Interface객체. 자세한 사항은 Interface항목 참조.
          callbackFunc : 요청 처리 한 후의 callback함수. – 인자값은 result 1개
리턴값 – 없음
사용방법 - interface객체는 다음과 같은 형식을 유지해야됩니다.
 
예시) 
• SetEXETimeOut(time);
InziEXE_GetReturn로 EXE 요청에 대한 리턴값을 기다리는 최대 시간을 설정합니다.
EXE에 대한 리턴이 없을 경우 해당 정해진 시간이 지나면 InziEXE_GetReturn의 콜백함수로 애러가 떨어집니다.
Default 값은 30분입니다.
파라미터 – time : 시간설정(ms)
리턴값 – 없음
사용방법 - 
예시)
 
• GetCustomURI ();
Inzi_MoisAgent.exe를 실행시키는 CustomURI값을 직접 얻습니다.
파라미터 - 없음
리턴값 – customURI string값
• getBrowserPos();
현재 브라우저의 좌표값을 가져오는 함수.
파라미터 - 없음
리턴값 - 좌표 string값 (“xpos,ypos,width,height”) 


 
Interface object

Key	설명	Value 종류
EXEKIND	핸들링 할 EXE 종류를 선택합니다.
[Number 값입니다.]	0 : 테스트
1: 화상 스캔
2: 본인확인
3: 십지지문
4: 서명 
COMMAND	EXE에 대한 명령을 내립니다.
[String 값입니다.]	Start : EXE를 실행시킵니다.
Close : EXE를 종료시킵니다.
PARAM	명령에 대한 인자값입니다.
반드시 각 EXE, COMMAND마다 정의가 되어 있어야 합니다.
[Object 값입니다.]	Ex) 
{
    param1 : value1,
    param2 : value2,
    param3 : value3
}
Or
Null

Ex) Interface 객체 생성 예
 
   Param 값 생성 예
 
InziEXE_GetReturn Callback 함수 에러코드
HTTP Response Return status
		200 : 성공 
			-> Callback json String 리턴 인자로 처리 할 것.
		0 : 서버 없음 
			-> InziEXE_Init()으로 다시 띄울 것
		500 : 서버 애러 
			-> TimeError 가능성 큼 SetEXETimeOut으로 timeout시간 늘릴 것.
                   그 외는 재설치 가이드 후 안되면 담당자 문의.
		508 : Agent아닌 프로그램 관련 에러. 메시지 확인 필요
			-> ErrorMessage
 		     1. exe file dosen't exist... {EXEFile}
			       -> 실행시킬 EXE File이 존재하지 않음. 설치문제이므로 재설치 안내 필요
			     2. exe is already started... {EXEFile}
 		       -> 실행시킨 EXE가 실행중임.
  		   	 개발자 판단하에 재실행 필요할 경우 
                           InziEXE_GetReturn 전달 Interface에 Command 값 Close로 기존 프로그램 종료
                           후 다시 실행시킬 것.
                     그 외는 재설치 가이드 후 다시해볼 것. 안되면 담당자 문의
		507 : 에러 메시지 확인 필요
                -> Interface 규약 안 지킴 Interface 객체 Key값은 다음 항목 참조

