
PDFS=charter.pdf

.PHONY: pdfs

view: pdfs
	evince *.pdf

pdfs: $(PDFS)

%.pdf: %.tex
	pdflatex -interaction=nonstopmode $<


clean:
	rm *.aux *.log *.pdf

