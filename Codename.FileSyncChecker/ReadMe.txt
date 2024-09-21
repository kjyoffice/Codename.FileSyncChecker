@@ 당장
날자검색 UI - 코드는 LunchArgs쪽에 지정되어 있으며 코드진행까지 되어있음
	날자검색 구조
		~ 일 까지 : MinDate ~ UserSetDate(End)
		일 ~ (Start)UserSetDate ~ MaxDate
		일 ~ 일 : StartDate ~ EndDate

리스트에 체크박스 두어서 제외항목 기능구현
싱크상태별로 컬러배합

(?) 대용량 파일인 경우, 프로그레스바 이동
	특정크기 이상의 파일이면 새로운 프로그레스바(컨티뉴)를 지정해서
	스트림으로 읽어들임
	현재 밀키웨이는 그냥 무조건 읽어들여서 대용량인 경우 프로그램 프리징이 발생

	밀키웨이 : 스트림으로의 기능을 추가

엑셀로 OUTPUT

@@ 다음으로
자동 업데이트
프로그램 진행 중 파일 변경내역 체크/반영

@@ 나아중~??
작업예외파일 지정 UI 및 코드진행 수정(1차적(app.config) 코드진행은 되어있음)
스플래쉬 스크린 (로딩시 업데이트 체크 진행 / 사용자 동의 (파일 MD5지정하여 수정여부 확인))

--------------------------------------

Clipboard.Clear();
Clipboard.SetText(XProvider.ResourceValue.EULABox.ControlValue.EULAContent, TextDataFormat.Text);
