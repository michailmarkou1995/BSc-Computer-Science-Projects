% Read Wisconsin Breast Cancer database
clear; clc; close all;

fh = fopen('data.csv','r');
Data = textscan(fh, ['%d,%[^,],',repmat('%f,',1,29),'%f\n'], 'HeaderLines',1);
fclose(fh);
n = length(Data);       % number of features + target
n = n-2;                % ignore first two attributes (ID, target)
P = length(Data{1});    % number of patterns
%% prepare Data sets
x = zeros(P,n);
for pat=1:P
    for i=1:n
        x(pat,i) = double(Data{i+2}(pat)); % fill row by row entering Data cell variable
    end
end

% fitsvm does not read String Labels sto Numeric Mapping instead
% row results if need to transpose to column use t = t'; or t = t.';
t = zeros(1,P); % targets
for pat=1:P
    if char(Data{2}(pat)) == 'M'
        t(pat) = 1; % Malignant
    else
        t(pat) = -1; %Benign
    end
end
t = t';

save('wdbc.mat', 'x', 't');