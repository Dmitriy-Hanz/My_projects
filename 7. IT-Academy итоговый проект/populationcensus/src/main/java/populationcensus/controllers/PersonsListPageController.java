package populationcensus.controllers;

import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;
import populationcensus.Consts;
import populationcensus.repository.entity.Person;
import populationcensus.service.interfaces.PersonService;

import java.util.LinkedList;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import java.util.stream.IntStream;

@Controller
@RequiredArgsConstructor
public class PersonsListPageController {

    private final PersonService personService;

    @GetMapping(Consts.Url.$_ADMIN_MAIN_$_PERSONS_LIST)
    public String load(Model model, @RequestParam("page") Optional<Integer> page, @RequestParam("size") Optional<Integer> size) {
        int currentPage = page.orElse(1);
        int pageSize = size.orElse(2);

        Page<Person> personPage = personService.findAllAndPaginate(PageRequest.of(currentPage - 1, pageSize));

        model.addAttribute("persons", personPage);

        int totalPages = personPage.getTotalPages();
        if (totalPages > 0) {
            List<Integer> pageNumbers = IntStream.rangeClosed(1, totalPages)
                    .boxed()
                    .collect(Collectors.toList());
            model.addAttribute("pageNumbers", pageNumbers);
        }

        int span = 2;
        List<Integer> spannedPages = new LinkedList<>();
        for (int i=currentPage, s = span+1; i > 0 && s>0; i--, s--) {
            spannedPages.add(i);
        }
        for (int i=currentPage+1, s = span; i < totalPages+1 && s > 0; i++, s--){
            spannedPages.add(i);
        }
        spannedPages.sort(Integer::compare);

        model.addAttribute("spannedPages", spannedPages);
        model.addAttribute("lastSpannedPage", spannedPages.get(spannedPages.size()-1));

        return "personsListPage";
    }

    @PostMapping("/blank/{id}")
    public String blank(@PathVariable String id){
        return "redirect:/adminMain/personsList/blank/{id}";
    }
}
