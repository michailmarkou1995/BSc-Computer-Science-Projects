% Read Wisconsin Breast Cancer database
clear; clc; close all;

fh = fopen('data.csv','r');
%Data = textscan(fh, ['%d,%[^,],',repmat('%f,',1,29),'%f\n']);
%Data = textscan(fh,'%f%f%f%f%f%f%f%f%f%f%f%f%f%f%f%f%f%f%f%f%f%f%f%f%f%f%f%f%f%f%f%f%f%f','Delimiter',',','HeaderLines',1);
fclose(fh);
n = length(Data);       % number of features + target
n = n-2;                % ignore first two attributes (ID, target)
P = length(Data{1});    % number of patterns
%%
x = zeros(n,P);
for pat=1:P
    for i=1:n
        x(i,pat) = double(Data{i+2}(pat));
    end
end

t = zeros(1,P); % targets
for pat=1:P
    if char(Data{2}(pat)) == 'M'
        t(pat) = 1;
    else
        t(pat) = -1;
    end
end

save('wdbc.mat', 'x', 't');