package populationcensus.controllers;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.SessionAttributes;
import populationcensus.Consts;
import populationcensus.dto.mapper.AccommodationsInfoMapper;
import populationcensus.repository.entity.AccommodationsInfo;
import populationcensus.service.interfaces.AccommodationsInfoService;

@Controller
@SessionAttributes("passportID")
@RequiredArgsConstructor
public class MainPageController {

    @GetMapping(Consts.Url.$_MAIN)
    public String load(){
        return "mainPage";
    }

    @PostMapping(Consts.Url.$_SURVEY_HOUSEHOLD)
    public String householdQuestionsPage(){
        return "redirect:" + Consts.Url.MAIN_$_SURVEY_HOUSEHOLD;
    }

    @PostMapping(Consts.Url.$_MY_BLANK)
    public String userBlankViewerPage(@RequestParam(name = "passportID") String passportID, Model model){
        model.addAttribute("passportID", passportID);
        return "redirect:" + Consts.Url.MAIN_$_MY_BLANK;
    }

    @PostMapping(Consts.Url.$_AUTHORISATION)
    public String authorization(){
        return "redirect:" + Consts.Url.ADMIN_MAIN;
    }
}
