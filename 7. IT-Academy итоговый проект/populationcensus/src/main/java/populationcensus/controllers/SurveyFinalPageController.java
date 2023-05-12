package populationcensus.controllers;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.support.SessionStatus;
import populationcensus.Consts;

import javax.servlet.http.HttpSession;

@Controller
public class SurveyFinalPageController {
    @GetMapping("*/surveyFinish")
    public String load(){
        return "questionsFinishPage";
    }

    @PostMapping(Consts.Url.$_FINISH)
    public String finish(HttpSession httpSession, SessionStatus status){
        status.setComplete();
        httpSession.removeAttribute("household");
        httpSession.removeAttribute("currentPerson");
        httpSession.removeAttribute("persons");
        return "redirect:" + Consts.Url.$_MAIN;
    }
}
